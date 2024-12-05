using HSS.Domain.IdentityModels;

namespace HSS.Domain.Models
{
    public class RadiologyCenter : BaseClass<int>
    {
        [Required]  // Ensures this field cannot be null
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        [Required]  // Ensures this field is mandatory
        [StringLength(200, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string Location { get; set; }

        //[Required]  // Ensures this field is mandatory
        //[MinLength(5, ErrorMessage = "At least one equipment is required.")]
        //[MaxLength(500, ErrorMessage = "Equipment list cannot exceed 500 characters.")]
        //public List<string> EquipmentList { get; set; } = new List<string>();

        [Required]
        public TimeSpan StartAt { get; set; }

        [Required]
        public TimeSpan EndAt { get; set; }

        [Required]  // Ensures this field is mandatory
        [Range(typeof(TimeSpan), "00:05:00", "02:00:00", ErrorMessage = "Appointment duration must be between 5 minutes and 2 hours.")]
        public TimeSpan AppointmentDuration { get; set; }
    }

}
