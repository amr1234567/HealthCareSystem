using HSS.Domain.Dtos;
using HSS.Services.SharedDto;

namespace HSS.Presentation.MVC.Models;

public class AppointmentDetailsViewModel
{
    public AppointmentPatientBaseData Appointment { get; set; }
    public int AppointmentId { set; get; }
    public AppointmentDetailsViewModel(AppointmentPatientBaseData appointment, int appointmentId)
    {
        Appointment = appointment;
        AppointmentId = appointmentId;
    }
}