using HSS.Domain.Models.ManyToManyRelationEntitys;

namespace HSS.Domain.Models
{
    public class Medicine : BaseClass<int>
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public MedicineType Type { get; set; }

        [Required]
        [StringLength(200)]
        public string Manufacturer { get; set; }

        public List<EffectiveSubstanceMedicine> EffectiveSubstance { get; set; }

        [Required]
        public int EffectiveSubstanceId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ApprovalDate { get; set; }

        [AllowNull]
        [StringLength(300)]
        public string StorageConditions { get; set; }

        [Required]
        public bool PrescriptionRequired { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public float Cost { get; set; }

        public List<SideEffect> SideEffects { get; set; }
    }
}
