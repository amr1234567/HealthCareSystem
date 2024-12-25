using System.ComponentModel.DataAnnotations;

namespace HSS.Presentation.MVC.Controllers;

public class EditAppointmentModel
{
    [Required]
    public int AppointmentId { get; set; }

    public bool IsQueue { get; set; } = false;
}