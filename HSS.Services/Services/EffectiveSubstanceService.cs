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
    public class EffectiveSubstanceService : IEffectiveSubstanceService
    {
        private readonly DbContext _dbContext;
        public EffectiveSubstanceService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateEffectiveSubstance(CreateEffectiveSubstanceDto effectiveSubstance)
        {
            try
            {
                if (effectiveSubstance is null)
                    throw new ArgumentNullException(nameof(effectiveSubstance));
                var newEffectiveSubstance = new EffectiveSubstance
                {
                    Name = effectiveSubstance.Name,
                    Description = effectiveSubstance.Description,
                    ChemicalFormula = effectiveSubstance.ChemicalFormula,
                    DiscoveryDate = effectiveSubstance.DiscoveryDate,
                    ApprovedBy = effectiveSubstance.ApprovedBy,
                    StabilityConditions = effectiveSubstance.StabilityConditions,
                    SideEffects = effectiveSubstance.SideEffects.Select(s=> SideEffectDto.ToModel(s)).ToList(),
                    PrimaryUsage = effectiveSubstance.PrimaryUsage,
                    AlternativeNames = effectiveSubstance.AlternativeNames,
                };
                await _dbContext.AddAsync(newEffectiveSubstance);
                await _dbContext.SaveChangesAsync();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEffectiveSubstance(int? id)
        {
            try
            {
                if (id is null)
                    throw new ArgumentNullException(nameof(id));
                await _dbContext.Set<EffectiveSubstance>().Where(x => x.Id == id).ExecuteDeleteAsync();
                await _dbContext.SaveChangesAsync();
                return true;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<EffectiveSubstanceDto>> GetAllEffectiveSubstance()
        {
            try
            {
                var effectiveSustances = await _dbContext.Set<EffectiveSubstance>()
                    .Select(x => new EffectiveSubstanceDto
                        {
                            Name = x.Name,
                            Description = x.Description,
                            ChemicalFormula = x.ChemicalFormula,
                            SideEffects = x.SideEffects.Select(s => new SideEffectDto(s)).ToList(),
                        }).ToListAsync();
                return effectiveSustances;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EffectiveSubstanceDto> GetEffectiveSubstanceById(int? id)
        {
            try
            {
                if (id is null)
                    throw new ArgumentNullException(nameof(id));

                var e = await _dbContext.Set<EffectiveSubstance>().FindAsync(id);

                if (e == null)
                    throw new ArgumentNullException(nameof(e));

                var effectiveSustance = new EffectiveSubstanceDto
                {
                    Name = e.Name,
                    Description = e.Description,
                    ChemicalFormula = e.ChemicalFormula,
                    SideEffects = e.SideEffects.Select(s => new SideEffectDto(s)).ToList(),
                };
                return effectiveSustance;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
