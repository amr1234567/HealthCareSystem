using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSS.Services.Dtos;

namespace HSS.Services.Abstractions
{
    public interface IClinicService
    {
        Task<bool> DeleteClinic(int id);
        Task<bool> UpdateClinic(int id, string Location, TimeSpan StartAt, TimeSpan FinshAt);
        Task<bool> CreateClinic(CreateClinicDto dto);
        Task<ClinicDto> GetClinic(int id);
        Task<List<ClinicDto>> HospitalClinics(int id);
    }
}
