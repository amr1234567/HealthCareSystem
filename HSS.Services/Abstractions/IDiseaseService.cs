using DiseaseService.Dtos;
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
        Task<IEnumerable<DiseaseDto>> GetAllDiseasesAsync();
        Task<DiseaseDto?> GetDiseaseByIdAsync(int id);
        Task<DiseaseDto> CreateDiseaseAsync(DiseaseDto disease);
        Task<DiseaseDto?> UpdateDiseaseAsync(int id, DiseaseDto disease);
        Task<bool> DeleteDiseaseAsync(int id);
    }
}
