using HSS.Domain.Models.ManyToManyRelationEntitys;

namespace HSS.Domain.Models
{
    public class Disease : BaseClass<int>
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public Severity Severity { get; set; }
        [Required]
        public bool Contagious { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Discovery_date { get; set; }
        [Required]
        public int AffectedPopulation { get; set; }
        [Required]
        public string DiseaseCode { get; set; }

        [Required, Range(0, 100)]
        public float CureRate { get; set; }

        [Required, Range(0, 100)]
        public float FatalityRate { get; set; }

        [Required, Range(0, int.MaxValue)]
        public string TreatmentDurationInDays { get; set; }

        [Required]
        public bool IsChronic { get; set; }

        [Required]
        public bool HasVaccine { get; set; }

        [Required]
        public AgeGroup CommonAgeGroup { get; set; }

        [Required]
        public Gender CommonGender { get; set; }

        [Required, MaxLength(300)]
        public string RiskFactors { get; set; }    // هل نعملها تيبل ونخزن فيها كل اشكال عرضة المرض ؟
        
        [AllowNull, MaxLength(200)]
        public string? GeographicSpread { get; set; }
        
        public DateTime? LastOutbreakDate { get; set; }
        
        [Required]
        public ResearchStatus ResearchStatus { get; set; }
        
        [Required, MaxLength(300)]
        public string PreventionMeasures { get; set; }
        
        [AllowNull, MaxLength(300)]
        public string? Notes { get; set; }

        public List<SymptomDisease> Symptoms { get; set; }
    }
}
