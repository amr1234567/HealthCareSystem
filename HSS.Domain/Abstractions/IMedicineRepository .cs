using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Abstractions
{
    public interface IMedicineRepository
    {
        Task<Medicine> GetMedicineByIdAsync(int id);
        Task<IEnumerable<Medicine>> GetAllMedicineAsync();
        Task<float> GetMedicineCostAsync(int id);
        Task<Medicine> GetMedicineMetaDataAsync(int id, string name);
        Task<IEnumerable<Medicine>> SearchForMedicineAsync(IEnumerable<Medicine> medicines, Func<Medicine> func);
        Task AddMedicineAsync(Medicine medicine);
        Task UpdateMedicineAsync(int id, Medicine medicine);
        Task DeleteMedicineAsync(int id);

    }
}