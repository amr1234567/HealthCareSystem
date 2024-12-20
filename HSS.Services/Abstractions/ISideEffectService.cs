using HSS.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.Abstractions
{
    public interface ISideEffectService
    {
        Task AddSideEffect(CreateSideEffectDto sideEffect);
        Task<bool> DeleteEffect(int? id);
        Task<IEnumerable<SideEffectDto>> GetAllSideEffects();
        Task<SideEffectDto> GetSideEffectById(int? id);
    }
}
