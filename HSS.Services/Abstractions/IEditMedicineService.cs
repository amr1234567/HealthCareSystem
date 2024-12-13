using HSS.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.Abstractions
{
    public interface IEditMedicineService
    {
        Task<bool> DeleteMedicineAsync(int? id);
        Task<bool> UpdateMedicineAsync(AddMedicineDto? newMedicine, int? id);
    }
}
