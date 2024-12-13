using HSS.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.Abstractions
{
    public interface IAddMedicineService
    {
        Task<bool> AddMedicine(AddMedicineDto medicine);
    }
}
