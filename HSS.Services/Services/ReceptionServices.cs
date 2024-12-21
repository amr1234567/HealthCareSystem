using HSS.DataAccess.Contexts;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using HSS.Domain.Models.Aggregates;
using HSS.Services.Abstractions;
using HSS.Services.Models;
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

    }
}
