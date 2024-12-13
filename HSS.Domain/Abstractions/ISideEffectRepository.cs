using HSS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Abstractions
{
    public interface ISideEffectRepository
    {
        Task AddSideEffectAsync(SideEffect effect);
        Task UpdateSideEffectAsync(SideEffect effect, int id);
        Task DeleteSideEffectAsync(int id);
        Task<IEnumerable<SideEffect>> GetAllSideEffectsAsync();
        Task<SideEffect?> GetSideEffectByIdAsync(int id);
    }
}
