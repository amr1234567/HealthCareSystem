using HSS.Domain.Dtos;
using HSS.Domain.Enums;
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
    public class CreateMedicineService : IAddMedicineService
    {
        private readonly DbContext _dbContext;
        public CreateMedicineService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddMedicine(AddMedicineDto newMedicine)
        {
            try
            {
                if (newMedicine is not null)
                {
                    var medicine = new Medicine
                    {
                        Name = newMedicine.Name,
                        Description = newMedicine.Description,
                        Type = newMedicine.Type,
                        Manufacturer = newMedicine.Manufacturer,
                        EffectiveSubstances = newMedicine.EffectiveSubstance,
                        SideEffects = newMedicine.SideEffects,
                        ApprovalDate = newMedicine.ApprovalDate,
                        StorageConditions = newMedicine.StorageConditions,
                    };
                    await _dbContext.AddAsync(medicine);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
