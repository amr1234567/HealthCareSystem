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
    public class EditeMedicineService : IEditMedicineService
    {
        private readonly DbContext _dbContext;
        public EditeMedicineService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> DeleteMedicineAsync(int? id)
        {
            try
            {
                if (id is null)
                    throw new ArgumentNullException(nameof(id));

                await _dbContext.Set<Medicine>().Where(x => x.Id == id).ExecuteDeleteAsync();
                return true;

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateMedicineAsync(AddMedicineDto? newMedicine, int? id)
        {
            try
            {
                if (id is null && newMedicine is null)
                    throw new ArgumentNullException();
                
                await _dbContext.Set<Medicine>().Where(x => x.Id == id)
                     .ExecuteUpdateAsync(m => m
                        .SetProperty(m => m.Name, newMedicine.Name)
                        .SetProperty(m => m.Description, newMedicine.Description)
                        .SetProperty(m => m.Type, newMedicine.Type)
                        .SetProperty(m => m.Manufacturer, newMedicine.Manufacturer)
                        .SetProperty(m => m.EffectiveSubstances, newMedicine.EffectiveSubstance)
                        .SetProperty(m => m.SideEffects, newMedicine.SideEffects)
                        .SetProperty(m => m.ApprovalDate, newMedicine.ApprovalDate)
                        .SetProperty(m => m.StorageConditions, newMedicine.StorageConditions)
                     );
                await _dbContext.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
