namespace HSS.Domain.Models.ManyToManyRelationEntitys
{
    public class SymptomDisease
    {
        [Required]
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }

        [Required]
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}