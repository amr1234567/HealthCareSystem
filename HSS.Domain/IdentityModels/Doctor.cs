using HSS.Domain.Models;

namespace HSS.Domain.IdentityModels
{
    public class Doctor : IdentityUser
    {
        [Required]  // Ensures this field cannot be null
        [DataType(DataType.Date)]  // Ensures the date is in proper date format
        public DateTime HireDate { get; set; }
        
        [DataType(DataType.Duration)] 
        public TimeSpan WorkingTime { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan StartAt { get; set; }

        public Hospital Hospital { get; set; }
        [Required]
        public int HospitalId { get; set; }

        [Required]  // Ensures this field cannot be null
        [Range(0, int.MaxValue, ErrorMessage = "Experience years must be a positive integer.")]
        public int ExperienceYears { get; set; } // ??

        [Required]  // Ensures this field cannot be null
        [StringLength(100, ErrorMessage = "Specialization cannot exceed 100 characters.")]
        public string Specialization { get; set; }

        [Required]  // Ensures this field cannot be null
        [Range(1, int.MaxValue, ErrorMessage = "ClinicId must be a positive integer.")]
        public int ClinicId { get; set; }

        // Navigation property to Clinic
        public Clinic Clinic { get; set; }
    }
}
