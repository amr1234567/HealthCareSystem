using System.ComponentModel.DataAnnotations;

namespace HSS.Presentation.MVC.Models
{
    public class DelayAppointmentModel
    {
        [Required]
        public string PatientNationalId { get; set; }
        [Required]
        public string NewDate { get; set; }
        [Required]
        public string NewTime { get; set; }
    }
}
