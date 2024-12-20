using HSS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Dtos
{
    public class SideEffectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public SideEffectDto()
        {
            
        }

        public SideEffectDto(SideEffect sideEffect)
        {
            Id = sideEffect.Id;
            Name = sideEffect.Name;
            Description = sideEffect.Description;
        }

        public static SideEffect ToModel(SideEffectDto s)
        {
            return new SideEffect
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            };
        }
    }
}
