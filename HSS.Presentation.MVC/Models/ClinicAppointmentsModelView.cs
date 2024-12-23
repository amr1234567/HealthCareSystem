using HSS.Domain.Enums;
using HSS.Services.SharedDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HSS.Presentation.MVC.Models
{
    public class ClinicAppointmentsModelView
    {
        public List<AppointmentDto> Appointments { get; set; }
        public int ClinicId { get; set; }
        public bool IsQueueList { set; get; }
        public List<SelectListItem> AppointmentTypes { get; set; }
        public ClinicAppointmentsModelView(List<AppointmentDto> appointments, int clinicId, bool isQueueList)
        {
            Appointments = appointments;
            ClinicId = clinicId;
            IsQueueList = isQueueList;
            AppointmentTypes = Enum.GetNames(typeof(AppointmentType)).Select(x => new SelectListItem(x, x)).ToList();
        }
    }
}
