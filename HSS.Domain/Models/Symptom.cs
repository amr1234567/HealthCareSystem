namespace HSS.Domain.Models
{
    public class Symptom : BaseClass<int>
    {
        [Required]  // Ensures this field cannot be null
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required]
        public AgeGroup Age { get; set; }

        [Required]  // Ensures this field is mandatory
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters.")]
        public string Description { get; set; }

        [Required]  // Ensures this field is mandatory
        public Severity Severity { get; set; }

        [Required]
        [Range(0,1000)]
        public double Duration { get; set; }

        [Required]
        public SymptomOnsetPattern OnsetPattern { get; set; }

        [Required]  // Ensures this field cannot be null
        public bool IsChronic { get; set; }

        [Required]  // Ensures this field cannot be null
        public bool TreatmentRequired { get; set; }
    }
}
