using HSS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Abstractions
{
    public interface IEffectiveSubstanceRepository
    {
        Task AddEffectiveSubstanceAsync(EffectiveSubstance effectiveSubstance);
        Task DeleteEffectiveSubstanceAsync(int id);
        Task UpdateEffectiveSubstanceAsync(EffectiveSubstance effectiveSubstance, int id);
        Task<IEnumerable<EffectiveSubstance>> GetAllEffectiveSubstancesAsync();
        Task<EffectiveSubstance?> GetEffectiveSubstanceAsync(int id);
    }
}
