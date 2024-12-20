using HSS.Domain.Models.ManyToManyRelationEntitys;
using HSS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Dtos
{
    public class AddMedicineDto 
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public MedicinesType Type { get; set; }

        [Required]
        [StringLength(200)]
        public string Manufacturer { get; set; }

        public List<EffectiveSubstanceMedicine> EffectiveSubstanceM { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ApprovalDate { get; set; }

        [AllowNull]
        [StringLength(300)]
        public string StorageConditions { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public float Cost { get; set; }
        public List<SideEffect> SideEffects { get; set; }
    }
}
