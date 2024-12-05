using HSS.Domain.Models;

namespace HSS.Domain.IdentityModels
{
    public class RadiologyManager : IdentityUser<int>
    {
        // Employment Info
        public DateTime HireDate { get; set; }
        public int ExperienceYears { get; set; }

        // Certification
        public string Certification { get; set; }

        // Working Time
        public TimeSpan WorkingTime { get; set; }

        // Radiology Center Association
        [Required]
        public int RadiologyCenterId { get; set; }

        public RadiologyCenter RadiologyCenter { get; set; }
    }
}
