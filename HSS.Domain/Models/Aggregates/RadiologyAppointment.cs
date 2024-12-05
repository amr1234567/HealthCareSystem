using HSS.Domain.IdentityModels;

namespace HSS.Domain.Models.Aggregates
{
    public class RadiologyAppointment : Appointment
    {
        [Required]  // Ensures this field cannot be null
        [Range(1, int.MaxValue, ErrorMessage = "RadiologyCenterId must be a positive integer.")]
        public int RadiologyCenterId { get; set; }
        public RadiologyCenter RadiologyCenter { get; set; }

        [Required]  // Ensures this field cannot be null
        [StringLength(100, ErrorMessage = "Test type cannot exceed 100 characters.")]
        public RadiologyTestType TestType { get; set; }

        [Required]  // Ensures this field cannot be null
        [Range(1, int.MaxValue, ErrorMessage = "RadiologyTesterId must be a positive integer.")]
        public int RadiologyTesterId { get; set; }
        public RadiologyManager RadiologyManager { get; set; }

        [StringLength(500, ErrorMessage = "Test result cannot exceed 500 characters.")]
        public string TestResult { get; set; }

    }

}
