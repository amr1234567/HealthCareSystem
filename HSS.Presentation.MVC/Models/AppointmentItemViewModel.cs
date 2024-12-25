using HSS.Services.SharedDto;

namespace HSS.Presentation.MVC.Models;

public class AppointmentItemViewModel(AppointmentDto appointment, bool isQueue = false)
{
    public bool IsQueueItem { set; get; } = isQueue;
    public AppointmentDto Appointment { set; get; } = appointment;
}