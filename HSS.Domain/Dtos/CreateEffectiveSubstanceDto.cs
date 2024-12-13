using HSS.Domain.Models.ManyToManyRelationEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Dtos
{
    public class CreateEffectiveSubstanceDto
    {
        
        public string Name { get; set; }

        
        public string ChemicalFormula { get; set; }

        
        public string Description { get; set; }

        
        public DateTime DiscoveryDate { get; set; }

        
        public string ApprovedBy { get; set; }

        
        public string StabilityConditions { get; set; }

        public List<SideEffectEffectiveSubstance> SideEffects { get; set; }

       
        public string PrimaryUsage { get; set; }

        
        public string AlternativeNames { get; set; }
    }
}
