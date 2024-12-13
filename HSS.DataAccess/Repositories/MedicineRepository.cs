using HSS.DataAccess.Contexts;
using HSS.Domain.Abstractions;
using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HSS.DataAccess.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ApplicationDbContext _context;
        public MedicineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMedicineAsync(Medicine mediicine)
        {
            await _context.Medicines.AddAsync(mediicine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMedicineAsync(int id)
        {
            await _context.Medicines.Where(x => x.Id == id ).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Medicine>> GetAllMedicineAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        public async Task<Medicine> GetMedicineByIdAsync(int id)
        {
            return await _context.Medicines.FindAsync(id);
        }

        public async Task<float> GetMedicineCostAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine is null)
                throw new Exception("");
            return medicine.Cost;
        }

        public async Task<IEnumerable<EffectiveSubstanceMedicine>> GetMedicineEffectiveSubstanceAsync(int MedicineId)
        {
            throw new NotImplementedException();
        }

        public async Task<Medicine> GetMedicineMetaDataAsync(int id, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SideEffect>> GetMedicineSideEffectsAsync(Func<Medicine> medicine)
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<Medicine>> SearchForMedicineAsync(IEnumerable<Medicine> medicines, Func<Medicine> func)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMedicineAsync(int medicineId, Medicine nmedicine)
        {
            var medicine = await _context.Medicines.FirstOrDefaultAsync(x => x.Id.Equals(medicineId));
            if (medicine is not null && nmedicine is not null)
            {
                medicine = nmedicine;
                _context.Medicines.Update(medicine);
                await _context.SaveChangesAsync();
            }
        }

    }
}
