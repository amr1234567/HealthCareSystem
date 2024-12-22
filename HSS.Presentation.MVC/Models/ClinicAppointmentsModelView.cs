using HSS.Services.SharedDto;

namespace HSS.Presentation.MVC.Models
{
    internal class ClinicAppointmentsModelView
    {
        public List<AppointmentDto> Appointments { get; set; }
        public int ClinicId { get; set; }
        public bool IsQueueList { set; get; }
        public ClinicAppointmentsModelView(List<AppointmentDto> appointments, int clinicId, bool isQueueList)
        {
            Appointments = appointments;
            ClinicId = clinicId;
            IsQueueList = isQueueList;
        }
    }
}
