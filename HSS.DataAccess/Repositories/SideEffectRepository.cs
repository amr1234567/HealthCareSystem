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
    public class SideEffectRepository : ISideEffectRepository
    {
        private readonly ApplicationDbContext _context;
        public SideEffectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddSideEffectAsync(SideEffect effect)
        {
            await _context.SideEffects.AddAsync(effect);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSideEffectAsync(int id)
        {
            await _context.SideEffects.Where(x => x.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SideEffect>> GetAllSideEffectsAsync()
        {
            return await _context.SideEffects.ToListAsync();
        }

        public async Task<SideEffect?> GetSideEffectByIdAsync(int id)
        {
            var sideEffect = await _context.SideEffects.FindAsync(id);
            if (sideEffect is null)
                return null;
            return sideEffect;
        }

       
    }
}

