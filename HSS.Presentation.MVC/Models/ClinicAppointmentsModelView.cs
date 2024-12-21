using HSS.Services.SharedDto;

namespace HSS.Presentation.MVC.Models
{
    internal class ClinicAppointmentsModelView
    {
        public List<AppointmentDto> Appointments { get; set; }
        public int ClinicId { get; set; }
        public ClinicAppointmentsModelView(List<AppointmentDto> appointments, int clinicId)
        {
            Appointments = appointments;
            ClinicId = clinicId;
        }
    }
}
