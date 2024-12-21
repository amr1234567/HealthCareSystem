using HSS.Services.Models;

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
    }
}