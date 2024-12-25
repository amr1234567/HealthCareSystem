using HSS.Services.SharedDto;

namespace HSS.Presentation.MVC.Models;

public class AppointmentDetailsViewModel
{
    public AppointmentDto Appointment { get; set; }
    public AppointmentDetailsViewModel(AppointmentDto appointment)
    {
        Appointment = appointment;
    }
}