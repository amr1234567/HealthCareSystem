using HSS.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.Abstractions
{
    public interface IEffectiveSubstanceService
    {
        Task CreateEffectiveSubstance(CreateEffectiveSubstanceDto effectiveSubstance);
        Task<bool> DeleteEffectiveSubstance(int? id);
        Task<IEnumerable<EffectiveSubstanceDto>> GetAllEffectiveSustance();
        Task<EffectiveSubstanceDto> GetEffectiveSustanceById(int? id);
    }
}
