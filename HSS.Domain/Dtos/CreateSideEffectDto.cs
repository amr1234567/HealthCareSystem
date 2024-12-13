using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Dtos
{
    public class CreateSideEffectDto
    {
        
        public string Name { get; set; }

       
        public string Description { get; set; }

        
        public AgeGroup AgeRange { get; set; }

        
        public Severity Severity { get; set; }

        
        public SideEffectCommonality Commonality { get; set; }

        
        public TimeSpan Duration { get; set; }

        
        public bool Reversibility { get; set; }

        
        public string Notes { get; set; }
    }
}
