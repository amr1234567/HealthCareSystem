using HSS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Abstractions
{
    public interface IDiseaseRepository
    {
        Task<Disease> GetByIdAsync(int id);
        Task<IEnumerable<Disease>> GetAllAsync();
        Task<IEnumerable<Disease>> SearchAsync(string name, string code);
        Task<IEnumerable<Disease>> FilterAsync(Severity? severity, bool? contagious, int? affectedPopulation);
        Task AddAsync(Disease disease);
        Task UpdateAsync(Disease disease);
        Task DeleteAsync(int id);
    }
}
