namespace HSS.Domain.Models.ManyToManyRelationEntitys
{
    public class SideEffectMedicine
    {
        [Required]
        public int SideEffectId { get; set; }
        public SideEffect SideEffect { get; set; }

        [Required]
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}