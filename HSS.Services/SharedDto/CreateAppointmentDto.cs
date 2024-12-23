
using System.ComponentModel.DataAnnotations;

namespace HSS.Services.SharedDto
{
    public class CreateAppointmentDto
    {
        [Required]
        public string AppointmentDate { get; set; }
        [Required]
        public string AppointmentTime { get; set; }
        public string? Notes { get; set; }
        [Required]
        public string NationalId { get; set; }
        [Required]
        public int ClinicId { get; set; }
        [Required]
        public string AppointmentType { get; set; }
        public string? ReasonForVisit { get; set; }
        public bool IsConfirmed { get; set; } = false;
    }
}
