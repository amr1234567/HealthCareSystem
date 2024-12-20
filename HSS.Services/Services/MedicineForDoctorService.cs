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
    public class MedicineForDoctorService : IMedicienForDoctorService
    {
        private readonly DbContext _dbContext;
        public MedicineForDoctorService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<MedicineForDoctorDto>> GetMedicineForDoctor(string name)
        {
            try
            {
                if (name is null)
                    throw new ArgumentNullException(nameof(name));

                var m = await _dbContext.Set<Medicine>().Where(x => x.Name == name).ToListAsync();

                if (m == null)
                    throw new NullReferenceException(nameof(m));

                var medicines = await _dbContext.Set<Medicine>().Where(x => x.Name == name)
                    .Select(m => new MedicineForDoctorDto
                      {
                          Id = m.Id,
                          Name = m.Name,
                          Description = m.Description,
                          Cost = m.Cost,
                      })
                    .ToListAsync();
                return medicines;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
