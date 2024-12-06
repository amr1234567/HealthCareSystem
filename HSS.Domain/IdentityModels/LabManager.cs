using HSS.Domain.Models;

namespace HSS.Domain.IdentityModels
{
    public class LabManager : IdentityUser<int>
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime HireDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public TimeSpan WorkingTime { get; set; }

        [Range(0, 50, ErrorMessage = "Experience years must be between 0 and 50.")]
        public int ExperienceYears { get; set; }

        [StringLength(500, ErrorMessage = "Certifications cannot exceed 500 characters.")]
        public string Certifications { get; set; }

        [Required]
        public int LabCenterId { get; set; } // Reference to the associated Lab Center
        public LabCenter LabCenter { get; set; }
    }
}
