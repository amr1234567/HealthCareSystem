namespace HSS.Domain.Models.ManyToManyRelationEntitys
{
    public class SideEffectEffectiveSubstance
    {
        [Required]
        public int SideEffectId { get; set; }
        public SideEffect SideEffect { get; set; }

        [Required]
        public int EffectiveSubstanceId { get; set; }
        public EffectiveSubstance EffectiveSubstance { get; set; }
    }
}
