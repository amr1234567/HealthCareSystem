using HSS.DataAccess.Contexts;
using HSS.Domain.Enums;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using HSS.Domain.Models.Aggregates;
using HSS.Services.Abstractions;
using HSS.Services.Models;
using HSS.Services.SharedDto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                var doctorName = activeDoctor?.Name ?? "لا يوجد طبيب متاح الان";
                clinicDtos.Add(new ClinicDto(clinic, doctorName));
            }

            return clinicDtos;
        }

        public async Task<DoctorDto> GetCurrentlyWorkingDoctorAsync(int clinicId)
        {
            var currentTime = DateTime.UtcNow.TimeOfDay;

            var doctors = await _context.Set<Doctor>()
                .Where(d => d.ClinicId == clinicId)
                .ToListAsync();

            var workingDoctor = doctors.FirstOrDefault(d => 
            {
                var endTime = d.StartAt.Add(d.WorkingTime);
                
                // Handle case where shift crosses midnight
                if (endTime < d.StartAt)
                {
                    return (currentTime >= d.StartAt) || (currentTime <= endTime);
                }
                
                return currentTime >= d.StartAt && currentTime <= endTime;
            });

            if (workingDoctor == null)
                return null;

            return new DoctorDto(workingDoctor);
        }

        public async Task<DoctorDto> GetCurrentlyWorkingDoctorAsync(int clinicId, TimeSpan date)
        {
            var currentTime = date;

            var doctors = await _context.Set<Doctor>()
                .Where(d => d.ClinicId == clinicId)
                .ToListAsync();

            var workingDoctor = doctors.FirstOrDefault(d =>
            {
                var endTime = d.StartAt.Add(d.WorkingTime);

                // Handle case where shift crosses midnight
                if (endTime < d.StartAt)
                {
                    return (currentTime >= d.StartAt) || (currentTime <= endTime);
                }

                return currentTime >= d.StartAt && currentTime <= endTime;
            });

            if (workingDoctor == null)
                return null;

            return new DoctorDto(workingDoctor);
        }

        public async Task<bool> RemoveFromQueue(int appointmentId)
        {
            return await _context.ClinicAppointment.Where(c => c.Id == appointmentId)
                .ExecuteUpdateAsync(c => c.SetProperty(app => app.IsConfirmed, false)) > 0;
        }

        public async Task<AppointmentDto?> StartAppointment(int appointmentId)
        {
            await _context.ClinicAppointment.Where(c => c.Id == appointmentId)
                .ExecuteUpdateAsync(c => c.SetProperty(app => app.IsStarted, true));
            return await _context.ClinicAppointment
                .Where(c => c.Id == appointmentId)
                .Include(c => c.Patient)
                .Include(c => c.Doctor)
                .Include(c => c.Clinic)
                .ThenInclude(c => c.Hospital)
                .Select(c => new AppointmentDto(c)).FirstOrDefaultAsync(); 
        }

        public async Task<bool> CreateAppointment(CreateAppointmentDto dto)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(x => x.NationalId == dto.NationalId);
            if (patient == null)
                throw new Exception("لا يوجد مريض بهذا المعرف");
            var clinic = await _context.Clinics.FirstOrDefaultAsync(x => x.Id == dto.ClinicId);
            if ( clinic == null)
                throw new Exception("لا يوجد عيادة بهذا المعرف");
            var doctor = await GetCurrentlyWorkingDoctorAsync(dto.ClinicId);
            if (doctor == null)
                throw new Exception("لا يوجد طبيب متاح في هذا التوقيت ");
            var appointment = new ClinicAppointment
            {
                Notes = dto.Notes,
                AppointmentDate = DateTime.Parse($"{dto.AppointmentDate} {dto.AppointmentTime}"),
                CreatedAt = DateTime.UtcNow,
                Duration = TimeSpan.FromMinutes(clinic.AppointmentDurationInMinutes),
                HospitalId = clinic.HospitalId,
                ClinicId = dto.ClinicId,
                Patient = patient,
                AppointmentType = (AppointmentType)Enum.Parse(typeof(AppointmentType), dto.AppointmentType),
                DoctorId = doctor.Id,
                ReasonForVisit = dto.ReasonForVisit,
                IsConfirmed = dto.IsConfirmed,
            };
            await _context.ClinicAppointment.AddAsync(appointment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CancelAppointment(int appointmentId)
        {
            return await _context.ClinicAppointment
                .Where(x => x.Id == appointmentId).ExecuteDeleteAsync() > 0;
        }
        public async Task<AppointmentDto?> ConfirmAppointment(int appointmentId)
        {
            await _context.ClinicAppointment
                .Where(x => x.Id == appointmentId)
                .ExecuteUpdateAsync(x => x.SetProperty(x => x.IsConfirmed, true));
            return await _context.ClinicAppointment
                .Where(c => c.Id == appointmentId)
                .Include(c => c.Patient)
                .Include(c => c.Doctor)
                .Include(c => c.Clinic)
                .ThenInclude(c => c.Hospital)
                .Select(c => new AppointmentDto(c)).FirstOrDefaultAsync();
        }

        public async Task<bool> DelayAppointment(string nationalId,DateTime dateTimeDelayTo)
        {
            var appointment = await _context.ClinicAppointment
                .Include(x => x.Patient)
                .FirstOrDefaultAsync(x => x.Patient.NationalId == nationalId);

            if (appointment.AppointmentDate <= DateTime.Now)
                throw new Exception("لا يمكن تاخير هذا الموعد.");
            appointment.AppointmentDate = dateTimeDelayTo;
            appointment.IsConfirmed = false;
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<List<AppointmentDto>> ClinicAppointmentsQueue(int clinicId)
        {
            var result = await _context
                .ClinicAppointment
                .Where(x => x.IsConfirmed == true && x.ClinicId == clinicId && x.AppointmentDate > DateTime.UtcNow)
                .Include(x => x.Patient)
                .Include(x => x.Clinic)
                .Include(x => x.Doctor)
                .Include(x => x.Hospital)
                .Select(x => new AppointmentDto(x))
                .ToListAsync();

            var queue = new PriorityQueue<AppointmentDto, double>();
            foreach(var item in result)
            {
                queue.Enqueue(item, Priority(item.ClinicStartAt, item.ClinicEndAt, item.AppointmentDate.Value, item.Duration));
            }
            var list = new List<AppointmentDto>();
            while (queue.Count > 0) { 
                list.Add(queue.Dequeue());
            }
            return list;
        }

        public async Task<List<AppointmentDto>> AllClinicAppointments(int clinicId)
        {
            var result = await _context.ClinicAppointment
                .Where(x => x.ClinicId == clinicId)
                .Where(x=> x.AppointmentDate > DateTime.UtcNow)
                .Include(x => x.Patient)
                .Include(x => x.Clinic)
                .Include(x => x.Doctor)
                .Include(x => x.Hospital)
                .OrderBy(x => x.AppointmentDate)
                .Select(x => new AppointmentDto(x))
                .ToListAsync();
            return result;
        }


        private double Priority(TimeSpan from, TimeSpan to, DateTime reserved, TimeSpan Duration)
        {
            var total = to - from;
            var lowestProirity = (to - from).TotalMinutes;
            var priority = (to - reserved.TimeOfDay).TotalMinutes;
            double test4 = lowestProirity - priority;
            return test4 / Duration.TotalMinutes;
        }

        public async Task<List<SelectListItem>> GetAvailableTimeSlots(int clinicId, DateTime date)
        {
            // Get clinic working hours and appointment duration
            var clinic = await _context.Clinics
                .FirstOrDefaultAsync(c => c.Id == clinicId);

            if (clinic == null)
                throw new Exception("Clinic not found");

            // Get all appointments for the specified date
            var existingAppointments = await _context.ClinicAppointment
                .Where(a => a.ClinicId == clinicId
                    && a.AppointmentDate.Date == date.Date)
                .Select(a => new { a.AppointmentDate, a.Duration })
                .ToListAsync();

            // Generate all possible time slots
            var availableSlots = new List<SelectListItem>();
            var baseDate = date.Date; // Use the input date as base
            var currentDateTime = baseDate.Add(clinic.StartAt);
            var endDateTime = baseDate.Add(clinic.FinishAt);

            while (currentDateTime.Add(TimeSpan.FromMinutes(clinic.AppointmentDurationInMinutes)) <= endDateTime)
            {
                var slotEndTime = currentDateTime.Add(TimeSpan.FromMinutes(clinic.AppointmentDurationInMinutes));

                // Check if the time slot overlaps with any existing appointment
                var isSlotAvailable = !existingAppointments.Any(a =>
                    (a.AppointmentDate <= currentDateTime && a.AppointmentDate.Add(a.Duration) > currentDateTime) ||
                    (a.AppointmentDate < slotEndTime && a.AppointmentDate.Add(a.Duration) >= slotEndTime));

                if (isSlotAvailable)
                {
                    availableSlots.Add(new SelectListItem
                    {
                        Value = currentDateTime.ToString("HH:mm"),
                        Text = $"{currentDateTime:hh:mm tt} - {slotEndTime:hh:mm tt}"
                    });
                }

                currentDateTime = currentDateTime.Add(TimeSpan.FromMinutes(clinic.AppointmentDurationInMinutes));
            }

            return availableSlots;
        }
    }
}
