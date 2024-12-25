using HSS.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.Abstractions
{
    public interface IGetMedicineService
    {
        Task<GetMedicineDto> GetMedicineByIdAsync(int id);
        Task<IEnumerable<GetMedicineDto>> GetAllMedicineAsync();
        Task<IEnumerable<GetMedicineDto>> GetAllMedicineByQueryAsync(string query = "");
    }
}
