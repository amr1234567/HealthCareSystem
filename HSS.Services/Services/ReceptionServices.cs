using HSS.DataAccess.Contexts;
using HSS.Domain.Models;
using HSS.Domain.Models.Aggregates;
using HSS.Services.Abstractions;
using HSS.Services.Models;
using Microsoft.EntityFrameworkCore;

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
                throw new NullReferenceException(nameof(hospital));
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
            Func<ClinicAppointment, bool> expression = ca =>
            {
                var result = ca.ClinicId == clinicId;
                if (dateStart == null)
                    result = result && ca.AppointmentDate.Date >= DateTime.Now.Date;
                else
                    result = result && ca.AppointmentDate.Date >= dateStart.Value.Date;

                if (dateEnd != null)
                    result = result && ca.AppointmentDate.Date <= dateEnd.Value.Date;

                return result;
            };

            if (dateStart == null)
                dateStart = DateTime.UtcNow;
            var appointments = await _context.Set<ClinicAppointment>()
                .Where(ca => expression(ca))
                .Include(ca => ca.Clinic)
                .Include(ca => ca.Doctor)
                .Select(ca => new ClinicAppointmentDto(ca)).ToListAsync();
            return appointments;
        }
    }
}
