using DiseaseService.Dtos;
using HSS.Domain.Enums;
using HSS.Domain.Models;

namespace DiseaseService.Abstractions
{
    public interface IDiseaseController
    {
        Task<IEnumerable<DiseaseDto>> GetAllDiseasesAsync();
        Task<Disease> GetDiseaseAsync(int id);
        Task<IEnumerable<Disease>> GetDiseasesAsync();
        Task<IEnumerable<Disease>> SearchDiseasesAsync(string name, string code);
        Task<IEnumerable<Disease>> FilterDiseasesAsync(Severity? severity, bool? contagious, int? affectedPopulation);
        Task AddDiseaseAsync(Disease disease);
        Task UpdateDiseaseAsync(Disease disease);
        Task DeleteDiseaseAsync(int id);
    }
}
