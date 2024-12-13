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
    public class GetMedicineService : IGetMedicineService
    {
        private readonly DbContext _dbContext;
        public GetMedicineService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<GetMedicineDto>> GetAllMedicineAsync()
        {
            try
            {
                var medicines = await _dbContext.Set<Medicine>().Select(m => new GetMedicineDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Type = m.Type,
                    EffectiveSubstanceM = m.EffectiveSubstanceM.ToList(),
                    SideEffects = m.SideEffects.ToList(),
                    StorageConditions = m.StorageConditions,
                    Cost = m.Cost,
                    ApprovalDate = m.ApprovalDate,
                }).ToListAsync();
                return medicines;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetMedicineDto> GetMedicineByIdAsync(int? id)
        {
            try
            {
                if (id is null)
                    throw new ArgumentNullException(nameof(id));

                var m = await _dbContext.Set<Medicine>().FindAsync(id);

                if (m == null)
                    throw new NullReferenceException(nameof(m));

                var medicine = new GetMedicineDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Manufacturer = m.Manufacturer,
                    Type = m.Type,
                    EffectiveSubstanceM = m.EffectiveSubstanceM.ToList(),
                    SideEffects = m.SideEffects.ToList(),
                    StorageConditions = m.StorageConditions,
                    Cost = m.Cost,
                    ApprovalDate = m.ApprovalDate
                };
                return medicine;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }  
        }
    }
}
