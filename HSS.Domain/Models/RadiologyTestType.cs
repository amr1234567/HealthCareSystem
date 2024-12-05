namespace HSS.Domain.Models
{
    public class RadiologyTestType : BaseClass<int>
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [AllowNull, MaxLength(500)]
        public string Description { get; set; }
    }
}