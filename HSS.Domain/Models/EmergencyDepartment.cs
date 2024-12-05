namespace HSS.Domain.Models
{
    public class EmergencyDepartment : BaseClass<int>
    {
        [Required]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        [Required, MaxLength(500)]
        public string Location { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Capacity { get; set; }

        public bool AmbulanceAvailability { get; set; }
    }
}
