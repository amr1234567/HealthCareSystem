
using HSS.Domain.IdentityModels;
using HSS.Services.Enums;


namespace HSS.Domain.Models
{
    public class Clinic : BaseClass<int>
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        
        [Required]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        public ClinicSpecialization Specialization { get; set; }

        [Required]
        public int SpecializationId { get; set; }

        [AllowNull, MaxLength(200)]
        public string? Location { get; set; } // وصف للمكان بالمستشفي

        public MedicalDepartment MedicalDepartment { get; set; }
        
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartAt { get; set; }
        
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan FinishAt { get; set; }

        [Required, Range(5, 90)]
        public int AppointmentDurationInMinutes { get; set; }

        public List<Doctor> Doctors { get; set; }
    }
}
