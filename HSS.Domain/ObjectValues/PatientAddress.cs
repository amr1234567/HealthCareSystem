namespace HSS.Domain.ObjectValues
{
    public class PatientAddress
    {
        [Required]
        public int HouseName { get; set; }
        [Required]
        [MinLength(5), MaxLength(200)]
        public string StreetName { get; set; }
        [Required]
        [MinLength(5), MaxLength(100)]
        public string City { get; set; }
        [Required]
        [MinLength(5), MaxLength(100)]
        public string State { get; set; }
    }
}