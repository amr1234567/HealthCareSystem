using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSS.Services.SharedDto;

namespace HSS.Services.Abstractions
{
    public interface IReceptionService
    {
        Task<bool> CreateAppointment(CreateAppointmentDto dto);
        Task<bool> DelayAppointment(DateTime time, int appointmentId);
        Task<bool> RemoveAppointment(int id);
        Task<bool> AddToQueue(int AppointmentId);
        Task<Queue> GetQueue(int id);
        Task<List<Queue>> AllQueues();

        Task<ReceptionDto> GetReceptionById(int id);
        Task<ReceptionDto> GetReceptionByHospital(int id);
        Task<bool> AddReception(AddReceptionDto dto);
        Task<bool> UpdateReception(string Location,TimeSpan StartAt,TimeSpan EndAt);
        Task<bool> DeleteReception(int id);

        
    }
}
