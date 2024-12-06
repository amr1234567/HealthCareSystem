
using HSS.Domain.IdentityModels;
using HSS.Domain.Models.ManyToManyRelationEntitys;

namespace HSS.Domain.Models
{
    public class Hospital : BaseClass<int>
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Location cannot exceed 300 characters.")]
        public string Location { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
        public int Capacity { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid contact number.")]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EstablishedDate { get; set; }

        [Required]
        public int HospitalAdminId { get; set; }
        public HospitalAdmin HospitalAdmin { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartAt { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EndAt { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Bed availability must be non-negative.")]
        public int BedAvailability { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Number of doctors must be non-negative.")]
        public int NumberOfDoctors { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Number of nurses must be non-negative.")]
        public int NumberOfNurses { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Departments count must be non-negative.")]
        public int DepartmentsCount { get; set; }

        [Required]
        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
        public float Latitude { get; set; }

        [Required]
        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
        public float Longitude { get; set; }

        [AllowNull]
        [Url(ErrorMessage = "Invalid URL.")]
        public string WebsiteUrl { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "License number cannot exceed 50 characters.")]
        public string LicenseNumber { get; set; }

        [AllowNull]
        [StringLength(50, ErrorMessage = "Tax identification number cannot exceed 50 characters.")]
        public string TaxIdentificationNumber { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public float Rating { get; set; } = 0;

        public List<ClinicSpecializationHospital> ClinicSpecializations { get; set; }
    }

}


