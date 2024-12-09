namespace HSS.Domain.Models.ManyToManyRelationEntitys
{
    public class EffectiveSubstanceMedicine
    {
        public int EffectiveSubstanceId { get; set; }
        public EffectiveSubstance EffectiveSubstance { get; set; }

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }

        [AllowNull, MaxLength(500)]
        public string Description { get; set; }
    }
}
