using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Dtos
{
    public class GetMedicineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Cost { get; set; }
        public string Manufacturer {  get; set; }
        public List<EffectiveSubstanceMedicine> EffectiveSubstanceM { get; set; }
        public List<SideEffect> SideEffects { get; set; }
        public MedicinesType Type { get; set; }
        public string StorageConditions { get; set; }
        public DateTime ApprovalDate { get; set; }


    }
}
