using HSS.Domain.Enums;
using HSS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.Abstractions
{
    public interface IDiseaseService
    {
        Task<Disease> GetDiseaseAsync(int id);
        Task<IEnumerable<Disease>> GetDiseasesAsync();
        Task<IEnumerable<Disease>> SearchDiseasesAsync(string name, string code);
        Task<IEnumerable<Disease>> FilterDiseasesAsync(Severity? severity, bool? contagious, int? affectedPopulation);
        Task AddDiseaseAsync(Disease disease);
        Task UpdateDiseaseAsync(Disease disease);
        Task DeleteDiseaseAsync(int id);
    }
}
