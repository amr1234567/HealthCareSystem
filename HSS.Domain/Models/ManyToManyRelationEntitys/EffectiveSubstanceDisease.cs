namespace HSS.Domain.Models.ManyToManyRelationEntitys
{
    public class EffectiveSubstanceDisease
    {
        public int EffectiveSubstanceId { get; set; }
        public EffectiveSubstance EffectiveSubstance { get; set; }

        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }

        [AllowNull, MaxLength(500)]
        public string Description { get; set; }
    }
}
