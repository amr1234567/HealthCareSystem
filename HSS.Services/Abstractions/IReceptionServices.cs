using HSS.Domain.Models.Aggregates;
using HSS.Services.Models;
using HSS.Services.SharedDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HSS.Services.Abstractions
{
    public interface IReceptionServices
    {
        Task<IEnumerable<ClinicAppointmentDto>> GetClinicAppointments(int clinicId, DateTime? dateStart, DateTime? dateEnd);
        Task<IEnumerable<ClinicDto>> GetClinics(int hospitalId, int specializationId);
        Task<IEnumerable<ClinicDto>> GetClinicsByReceptionistIdAsync(int receptionistId, int specializationId);
        Task<DoctorDto> GetCurrentlyWorkingDoctorAsync(int clinicId);
        Task<ReceptionDto> GetReception(int hospitalId);
        Task<IEnumerable<SpecializationDto>> GetSpecializationsAsync(int hospitalId);
        Task<IEnumerable<SpecializationDto>> GetSpecializationsByReceptionistIdAsync(int receptionistId);
        Task<bool> CreateAppointment(CreateAppointmentDto dto);
        Task<AppointmentDto?> ConfirmAppointment(int NationalId);
        Task<bool> CancelAppointment(int appointmentId);
        Task<bool> DelayAppointment(string NationalId, DateTime dateTimeDelayTo);
        Task<List<AppointmentDto>> ClinicAppointmentsQueue(int clinicId);
        Task<List<AppointmentDto>> AllClinicAppointments(int clinicId);
        Task<List<SelectListItem>> GetAvailableTimeSlots(int clinicId, DateTime date);
        Task<DoctorDto> GetCurrentlyWorkingDoctorAsync(int clinicId, TimeSpan date);
        Task<bool> RemoveFromQueue(int appointmentId);
        Task<AppointmentDto?> StartAppointment(int appointmentId);
    }
}