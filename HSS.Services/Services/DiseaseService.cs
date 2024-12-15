using HSS.Domain.Dtos;
using HSS.Domain.Enums;
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
    public class DiseaseService : IDiseaseService
    {
        private readonly DbContext _context;

        public DiseaseService(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DiseaseDto>> GetAllDiseasesAsync()
        {
            try
            {
                return await _context.Set<Disease>()
                    .Select(d => new DiseaseDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        Severity = d.Severity.ToString(),
                        Contagious = d.Contagious,
                        DiscoveryDate = d.Discovery_date,
                        AffectedPopulation = d.AffectedPopulation,
                        DiseaseCode = d.DiseaseCode,
                        CureRate = d.CureRate,
                        FatalityRate = d.FatalityRate,
                        TreatmentDurationInDays = d.TreatmentDurationInDays,
                        IsChronic = d.IsChronic,
                        HasVaccine = d.HasVaccine,
                        CommonAgeGroup = d.CommonAgeGroup.ToString(),
                        CommonGender = d.CommonGender.ToString(),
                        RiskFactors = d.RiskFactors,
                        GeographicSpread = d.GeographicSpread,
                        LastOutbreakDate = d.LastOutbreakDate,
                        ResearchStatus = d.ResearchStatus.ToString(),
                        PreventionMeasures = d.PreventionMeasures,
                        Notes = d.Notes
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving diseases.", ex);
            }
        }

        public async Task<DiseaseDto?> GetDiseaseByIdAsync(int id)
        {
            try
            {
                var disease = await _context.Set<Disease>().FindAsync(id);
                if (disease == null)
                    return null;

                return new DiseaseDto
                {
                    Id = disease.Id,
                    Name = disease.Name,
                    Description = disease.Description,
                    Severity = disease.Severity.ToString(),
                    Contagious = disease.Contagious,
                    DiscoveryDate = disease.Discovery_date,
                    AffectedPopulation = disease.AffectedPopulation,
                    DiseaseCode = disease.DiseaseCode,
                    CureRate = disease.CureRate,
                    FatalityRate = disease.FatalityRate,
                    TreatmentDurationInDays = disease.TreatmentDurationInDays,
                    IsChronic = disease.IsChronic,
                    HasVaccine = disease.HasVaccine,
                    CommonAgeGroup = disease.CommonAgeGroup.ToString(),
                    CommonGender = disease.CommonGender.ToString(),
                    RiskFactors = disease.RiskFactors,
                    GeographicSpread = disease.GeographicSpread,
                    LastOutbreakDate = disease.LastOutbreakDate,
                    ResearchStatus = disease.ResearchStatus.ToString(),
                    PreventionMeasures = disease.PreventionMeasures,
                    Notes = disease.Notes
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving the disease with ID {id}.", ex);
            }
        }

        public async Task<DiseaseDto> CreateDiseaseAsync(DiseaseDto disease)
        {
            try
            {
                var entity = new Disease
                {
                    Name = disease.Name,
                    Description = disease.Description,
                    Severity = Enum.Parse<Severity>(disease.Severity),
                    Contagious = disease.Contagious,
                    Discovery_date = disease.DiscoveryDate,
                    AffectedPopulation = disease.AffectedPopulation,
                    DiseaseCode = disease.DiseaseCode,
                    CureRate = disease.CureRate,
                    FatalityRate = disease.FatalityRate,
                    TreatmentDurationInDays = disease.TreatmentDurationInDays,
                    IsChronic = disease.IsChronic,
                    HasVaccine = disease.HasVaccine,
                    CommonAgeGroup = Enum.Parse<AgeGroup>(disease.CommonAgeGroup),
                    CommonGender = Enum.Parse<Gender>(disease.CommonGender),
                    RiskFactors = disease.RiskFactors,
                    GeographicSpread = disease.GeographicSpread,
                    LastOutbreakDate = disease.LastOutbreakDate,
                    ResearchStatus = Enum.Parse<ResearchStatus>(disease.ResearchStatus),
                    PreventionMeasures = disease.PreventionMeasures,
                    Notes = disease.Notes
                };

                _context.Add(entity);
                await _context.SaveChangesAsync();

                disease.Id = entity.Id;
                return disease;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the disease.", ex);
            }
        }

        public async Task<DiseaseDto?> UpdateDiseaseAsync(int id, DiseaseDto disease)
        {
            try
            {
                var entity = await _context.Set<Disease>().FindAsync(id);
                if (entity == null)
                    return null;

                entity.Name = disease.Name;
                entity.Description = disease.Description;
                entity.Severity = Enum.Parse<Severity>(disease.Severity);
                entity.Contagious = disease.Contagious;
                entity.Discovery_date = disease.DiscoveryDate;
                entity.AffectedPopulation = disease.AffectedPopulation;
                entity.DiseaseCode = disease.DiseaseCode;
                entity.CureRate = disease.CureRate;
                entity.FatalityRate = disease.FatalityRate;
                entity.TreatmentDurationInDays = disease.TreatmentDurationInDays;
                entity.IsChronic = disease.IsChronic;
                entity.HasVaccine = disease.HasVaccine;
                entity.CommonAgeGroup = Enum.Parse<AgeGroup>(disease.CommonAgeGroup);
                entity.CommonGender = Enum.Parse<Gender>(disease.CommonGender);
                entity.RiskFactors = disease.RiskFactors;
                entity.GeographicSpread = disease.GeographicSpread;
                entity.LastOutbreakDate = disease.LastOutbreakDate;
                entity.ResearchStatus = Enum.Parse<ResearchStatus>(disease.ResearchStatus);
                entity.PreventionMeasures = disease.PreventionMeasures;
                entity.Notes = disease.Notes;

                await _context.SaveChangesAsync();

                return disease;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while updating the disease with ID {id}.", ex);
            }
        }

        public async Task<bool> DeleteDiseaseAsync(int id)
        {
            try
            {
                var entity = await _context.Set<Disease>().FindAsync(id);
                if (entity == null)
                    return false;

                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the disease with ID {id}.", ex);
            }
        }
    }
}
    
