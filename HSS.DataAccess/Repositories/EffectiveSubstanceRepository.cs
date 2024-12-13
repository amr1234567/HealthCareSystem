using HSS.DataAccess.Contexts;
using HSS.Domain.Abstractions;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.DataAccess.Repositories
{
    public class EffectiveSubstanceRepository : IEffectiveSubstanceRepository
    {
        private readonly ApplicationDbContext _context;
        public EffectiveSubstanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddEffectiveSubstanceAsync(EffectiveSubstance effectiveSubstance)
        {
            await _context.EffectiveSubstances.AddAsync(effectiveSubstance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEffectiveSubstanceAsync(int id)
        {
            await _context.EffectiveSubstances.Where(x => x.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EffectiveSubstance>> GetAllEffectiveSubstancesAsync()
        {
            return await _context.EffectiveSubstances.ToListAsync();
        }

        public async Task<EffectiveSubstance?> GetEffectiveSubstanceAsync(int id)
        {
            var effectiveSubs = await _context.EffectiveSubstances.FindAsync(id);
            if (effectiveSubs is null)
                return null;
            return effectiveSubs;
        }

        public async Task UpdateEffectiveSubstanceAsync(EffectiveSubstance nEffectiveSubstance, int id)
        {
            var effectiveSubstance = _context.EffectiveSubstances.FirstOrDefault(x => x.Id == id);
            if (effectiveSubstance is not null)
            {
                effectiveSubstance = nEffectiveSubstance;
                _context.EffectiveSubstances.Update(effectiveSubstance);
                await _context.SaveChangesAsync();
            }

        }
    }
}
