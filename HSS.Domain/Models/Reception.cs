using HSS.Domain.IdentityModels;

namespace HSS.Domain.Models
{
    public class Reception : BaseClass<int>
    {
        [Required]  // Ensures this field is mandatory
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        [Required]  // Ensures this field cannot be null
        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string Location { get; set; }

        [Required]
        public TimeSpan StartAt { get; set; }

        [Required]
        public TimeSpan EndAt { get; set; }

    }
}
