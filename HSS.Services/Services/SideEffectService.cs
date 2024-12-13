using HSS.Domain.Dtos;
using HSS.Domain.Models;
using HSS.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.Services
{
    public class SideEffectService : ISideEffectService
    {
        private readonly DbContext _dbContext;
        public SideEffectService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddSideEffect(CreateSideEffectDto sideEffect)
        {
            try
            {
                if (sideEffect is null)
                    throw new ArgumentNullException(nameof(sideEffect));

                var newsideEffect = new SideEffect
                {
                    Name = sideEffect.Name,
                    Description = sideEffect.Description,
                    AgeRange = sideEffect.AgeRange,
                    Commonality = sideEffect.Commonality,
                    Notes = sideEffect.Notes,
                    Severity = sideEffect.Severity,
                    Duration = sideEffect.Duration,
                    Reversibility = sideEffect.Reversibility,
                };
                await _dbContext.AddAsync(newsideEffect);
                await _dbContext.SaveChangesAsync();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEffect(int? id)
        {
            try
            {
                if (id is null)
                    throw new ArgumentNullException(nameof(id));

                await _dbContext.Set<SideEffect>().Where(x => x.Id == id).ExecuteDeleteAsync();
                await _dbContext.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<SideEffectDto>> GetAllSideEffects()
        {
            try
            {
                var sideEffects = await _dbContext.Set<SideEffect>()
                    .Select(x => new SideEffectDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                    }).ToListAsync();
                return sideEffects;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SideEffectDto> GetSideEffectById(int? id)
        {
            try
            {
                if (id is null)
                    throw new ArgumentNullException(nameof(id));

                var s = await _dbContext.Set<SideEffect>().FindAsync(id);

                if (s is null)
                    throw new ArgumentNullException(nameof(s));

                var sideEffect = new SideEffectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                };
                return sideEffect;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
