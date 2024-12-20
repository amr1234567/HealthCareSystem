﻿
using HSS.Domain.Models.ManyToManyRelationEntitys;

namespace HSS.Domain.Models
{
    public class EffectiveSubstance : BaseClass<int>
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }

        [AllowNull, MaxLength(500)]
        public string? ChemicalFormula { get; set; }

        [AllowNull, MaxLength(500)]
        public string? Description { get; set; }

        [AllowNull]
        [DataType(DataType.Time)]
        public DateTime? DiscoveryDate { get; set; }

        [Required]
        public string ApprovedBy { get; set; }

        [Required, MaxLength(500)]
        public string StabilityConditions { get; set; }

        public List<SideEffect> SideEffects { get; set; }

        [Required, MaxLength(300)]
        public string PrimaryUsage { get; set; }
        
        [AllowNull, MaxLength(500)]
        public string? AlternativeNames { get; set; }

        public List<Medicine> Medicines { get; set; }
        public List<EffectiveSubstance> EffectiveSubstances { get; set; }
    }
}
