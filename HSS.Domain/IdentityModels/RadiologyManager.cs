using HSS.Domain.Models;

namespace HSS.Domain.IdentityModels
{
    public class RadiologyManager : IdentityUser<int>
    {
        // Employment Info
        [DataType(DataType.DateTime)]
        public DateTime HireDate { get; set; }
        [Required]
        public int ExperienceYears { get; set; }

        // Certification
        [Required]
        [MaxLength(300)]
        public string Certification { get; set; }

        // Working Time
        [DataType(DataType.Time)]
        public TimeSpan WorkingTime { get; set; }

        // Radiology Center Association
        [Required]
        public int RadiologyCenterId { get; set; }

        public RadiologyCenter RadiologyCenter { get; set; }
    }
}
