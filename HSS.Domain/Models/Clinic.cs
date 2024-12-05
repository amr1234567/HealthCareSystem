
namespace HSS.Domain.Models
{
    public class Clinic : BaseClass<int>
    {
        [Required]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        public ClinicSpecialization Specialization { get; set; }
        [Required]
        public string SpecializationName { get; set; }
        [Required]
        public int SpecializationId { get; set; }

        [AllowNull, MaxLength(200)]
        public string Location { get; set; } // وصف للمكان بالمستشفي
        
        [Required]
        public TimeSpan StartAt { get; set; }
        
        [Required]
        public TimeSpan FinishAt { get; set; }

        [Required, Range(5, 90)]
        public int AppointmentDurationInMinutes { get; set; }
    }
}
