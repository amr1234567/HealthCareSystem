namespace HSS.Domain.Models
{
    public class SideEffect : BaseClass<int>
    {
        [Required]  // Ensures this field cannot be null
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]  // Ensures this field cannot be null
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; }

        [Required]
        public AgeGroup AgeRange { get; set; }

        [Required]  // Ensures this field is mandatory
        public Severity Severity { get; set; }

        [Required]  // Ensures this field is mandatory
        public SideEffectCommonality Commonality { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Duration { get; set; }

        [Required]  // Ensures this field is mandatory
        public bool Reversibility { get; set; }

        [AllowNull]
        [StringLength(500, ErrorMessage = "Notes cannot be longer than 500 characters.")]
        public string Notes { get; set; }

        public List<EffectiveSubstance> EffectiveSubstances { get; set; }
        public List<Medicine> Medicines { get; set; }
    }
}
