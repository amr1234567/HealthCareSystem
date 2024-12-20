using HSS.Services.Models;
using HSS.Services.SharedDto;

namespace HSS.Services.Abstractions
{
    public interface IReceptionServices
    {
        Task<IEnumerable<ClinicAppointmentDto>> GetClinicAppointments(int clinicId, DateTime? dateStart, DateTime? dateEnd);
        Task<IEnumerable<ClinicDto>> GetClinics(int hospitalId, int specializationId);
        Task<ReceptionDto> GetReception(int hospitalId);
        Task<IEnumerable<SpecializationDto>> GetSpecializationsAsync(int hospitalId);
        Task<bool> CreateAppointment(CreateAppointmentDto dto);
        Task<bool> ConfirmAppointment(string NationalId);
        Task<bool> CancelAppointemen(string NationalId);
        Task<bool> DelayAppointment(string NationalId, DateTime dateTimeDelayTo);
        Task<List<AppointmentDto>> clinicAppointments(int clinicId);
    }
}