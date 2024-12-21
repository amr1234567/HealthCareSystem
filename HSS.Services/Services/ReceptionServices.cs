using HSS.DataAccess.Contexts;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using HSS.Domain.Models.Aggregates;
using HSS.Services.Abstractions;
using HSS.Services.Models;
using HSS.Services.SharedDto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HSS.Services.Services
{
    public class ReceptionServices(ApplicationDbContext _context) : IReceptionServices
    {
        public async Task<ReceptionDto> GetReception(int hospitalId)
        {
            try
            {
                var reception = await _context.Set<Reception>().Where(r => r.HospitalId == hospitalId)
                    .Include(r => r.Hospital).Select(r => new ReceptionDto(r)).FirstOrDefaultAsync();
                if (reception == null)
                    throw new NullReferenceException(nameof(reception));
                return reception;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SpecializationDto>> GetSpecializationsAsync(int hospitalId)
        {
            var hospital = await _context.Set<Hospital>().Where(h => h.Id == hospitalId)
                .Include(h => h.ClinicSpecializations)
                .FirstOrDefaultAsync();
            if (hospital == null)
                return [];

            var specializations = hospital.ClinicSpecializations.Select(s => new SpecializationDto(s));
            return specializations;
        }

        public async Task<IEnumerable<ClinicDto>> GetClinics(int hospitalId, int specializationId)
        {
            var clinics = await _context.Clinics
                .Where(c => c.HospitalId == hospitalId && c.SpecializationId == specializationId)
                .Include(c => c.Specialization)
                .Include(c => c.Hospital)
                .Select(c => new ClinicDto(c)).ToListAsync();
            return clinics;
        }

        public async Task<IEnumerable<ClinicAppointmentDto>> GetClinicAppointments
            (int clinicId, DateTime? dateStart, DateTime? dateEnd)
        {
            var query = _context.Set<ClinicAppointment>().AsNoTracking().AsQueryable();

            if (dateStart == null)
                dateStart = DateTime.UtcNow;

            query = query.Where(ca => ca.ClinicId == clinicId && ca.AppointmentDate.Date >= dateStart.Value.Date);

            if (dateEnd != null)
                query = query.Where(ca => ca.AppointmentDate.Date <= dateEnd.Value.Date);

            var appointments = await query
                .Include(ca => ca.Clinic)
                .Include(ca => ca.Doctor)
                .Select(ca => new ClinicAppointmentDto(ca)).ToListAsync();
            return appointments;
        }

        public async Task<IEnumerable<SpecializationDto>> GetSpecializationsByReceptionistIdAsync(int receptionistId)
        {
            var hospital = await _context.Set<Receptionist>()
               .Where(rs => rs.Id == receptionistId)
               .Include(rs => rs.Reception)
                    .ThenInclude(r => r.Hospital)
                        .ThenInclude(h => h.ClinicSpecializations)
               .Select(rs => rs.Reception.Hospital)
               .FirstOrDefaultAsync();

            if (hospital == null)
                return [];

            var specializations = hospital.ClinicSpecializations.Select(s => new SpecializationDto(s));
            return specializations;
        }

        public async Task<IEnumerable<ClinicDto>> GetClinicsByReceptionistIdAsync(int receptionistId, int specializationId)
        {
            var receptionist = await _context.Set<Receptionist>()
               .Where(rs => rs.Id == receptionistId)
               .Include(rs => rs.Reception)
               .FirstOrDefaultAsync();

            if (receptionist == null)
                return [];

            var clinics = await _context.Clinics
              .Where(c => c.HospitalId == receptionist.Reception.HospitalId && c.SpecializationId == specializationId)
              .Include(c => c.Specialization)
              .Include(c => c.Hospital)
              .Include(c => c.Doctors)
              .ToListAsync();

            var clinicDtos = new List<ClinicDto>();
            foreach (var clinic in clinics)
            {
                var activeDoctor = await GetCurrentlyWorkingDoctorAsync(clinic.Id);
                var doctorName = activeDoctor?.Name ?? "No doctor available";
                clinicDtos.Add(new ClinicDto(clinic, doctorName));
            }

            return clinicDtos;
        }

        public async Task<DoctorDto> GetCurrentlyWorkingDoctorAsync(int clinicId)
        {
            var currentTime = DateTime.UtcNow.TimeOfDay;
            var doctor = await _context.Set<Doctor>()
                .Where(d => d.ClinicId == clinicId && d.StartAt <= currentTime && d.StartAt.Add(d.WorkingTime) >= currentTime)
                .FirstOrDefaultAsync();

            if (doctor == null)
                return null;

            return new DoctorDto(doctor);
        }

        public async Task<bool> CreateAppointment(CreateAppointmentDto dto)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x=>x.NationalId== dto.NationalId);
            var appointment = new ClinicAppointment
            {
                Notes = dto.Notes,
                AppointmentDate = dto.AppointmentDate,
                CreatedAt=DateTime.UtcNow,
                Duration=dto.Duration,
                HospitalId = dto.HospitalId,
                ClinicId=dto.ClinicId,
                Patient= patient,
                
            };
            await _context.ClinicAppointment.AddAsync(appointment);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> CancelAppointemen(string NationalId)
        {
            return await _context.ClinicAppointment.Include(x=>x.Patient).Where(x=>x.Patient.NationalId==NationalId).ExecuteDeleteAsync()>0;
        }
        public async Task<bool> ConfirmAppointment(string NationalId)
        {
            return await _context.ClinicAppointment
                .Include(x => x.Patient)
                .Where(x => x.Patient.NationalId == NationalId)
                .ExecuteUpdateAsync(x=>x.SetProperty(x=>x.IsConfirmed,true)) >0;
        }
        public async Task<bool> DelayAppointment(string NationalId,DateTime dateTimeDelayTo)
        {
            var appointment = await _context.ClinicAppointment
                .Include(x => x.Patient)
                .FirstOrDefaultAsync(x => x.Patient.NationalId == NationalId);

            if (appointment.AppointmentDate <= DateTime.Now)
                throw new Exception("لا يمكن تاخير هذا الموعد.");
            appointment.AppointmentDate = dateTimeDelayTo;
            return await _context.SaveChangesAsync()>0;
        }
        public async Task<List<AppointmentDto>> clinicAppointments(int clinicId)
        {
            var result =await _context
                .ClinicAppointment
                .Include(x => x.Patient)
                .Where(x => x.IsConfirmed == true && x.ClinicId == clinicId)
                .Select(x=>new AppointmentDto
                {
                    NationalId=x.Patient.NationalId,
                    ClinicName=x.Clinic.SpecializationName,
                    PatientName=x.Patient.Name,
                    Reserved=x.AppointmentDate,
                    EndAt=x.Clinic.FinishAt,
                    StartAt=x.Clinic.StartAt,
                    Duration=x.Duration
                })
                .ToListAsync();

            var queue = new PriorityQueue<AppointmentDto, double>();
            foreach(var item in result)
            {
                queue.Enqueue(item, Priority(item.StartAt, item.EndAt,item.Reserved,item.Duration));
            }
            var list = new List<AppointmentDto>();
            while (queue.Count > 0) { 
                list.Add(queue.Dequeue());
            }
            return list;
        }
        private double Priority(TimeSpan from, TimeSpan to, DateTime reserved, TimeSpan Duration)
        {
            var total = to - from;
            var lowestProirity = (to - from).TotalMinutes;
            var priority = (to - reserved.TimeOfDay).TotalMinutes;
            double test4 = lowestProirity - priority;
            return test4 / Duration.TotalMinutes;
        }
    }
}
