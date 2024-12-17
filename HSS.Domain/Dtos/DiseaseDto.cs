namespace HSS.Domain.Dtos
{
    public class DiseaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public bool Contagious { get; set; }
        public DateTime DiscoveryDate { get; set; }
        public int AffectedPopulation { get; set; }
        public string DiseaseCode { get; set; }
        public float CureRate { get; set; }
        public float FatalityRate { get; set; }
        public string TreatmentDurationInDays { get; set; }
        public bool IsChronic { get; set; }
        public bool HasVaccine { get; set; }
        public string CommonAgeGroup { get; set; }
        public string CommonGender { get; set; }
        public string RiskFactors { get; set; }
        public string? GeographicSpread { get; set; }
        public DateTime? LastOutbreakDate { get; set; }
        public string ResearchStatus { get; set; }
        public string PreventionMeasures { get; set; }
        public string? Notes { get; set; }
    }
}
