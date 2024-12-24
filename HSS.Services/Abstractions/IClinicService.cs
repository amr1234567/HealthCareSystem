using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSS.Domain.Dtos;
using HSS.Domain.Models.Aggregates;
using HSS.Services.Dtos;
using HSS.Services.SharedDto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HSS.Services.Abstractions
{
    public interface IClinicService
    {
        Task<bool> DeleteClinic(int id);
        Task<bool> UpdateClinic(int id, string Location, TimeSpan StartAt, TimeSpan FinshAt);
        Task<bool> CreateClinic(CreateClinicDto dto);
        Task<ClinicDto> GetClinic(int id);
        Task<List<ClinicDto>> HospitalClinics(int id);
        Task<List<AppointmentDto>> ClinicAppointments(int doctorId);
        Task<bool> AppointmentFinished(int appointmentId);
        Task<AppointmentPatientBaseData> GetAppointmentPatientBaseData(string NationalId);
        Task<bool> AddRadiologyAppointment(int appointmentId, int radioId);
        Task<bool> AddTestAppointment(int appointmentId, int testId);
        Task<List<SelectListItem>> Medicines(string? medicine);
        Task<List<SelectListItem>> RadiologyTypes(string? type);
        Task<List<SelectListItem>> TestTypes(string? type);
    }
}
