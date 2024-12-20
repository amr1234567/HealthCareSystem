using HSS.Domain.Models.ManyToManyRelationEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Dtos
{
    public class EffectiveSubstanceDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ChemicalFormula { get; set; }
        public List<SideEffectEffectiveSubstance> SideEffects { get; set; }
    }
}
