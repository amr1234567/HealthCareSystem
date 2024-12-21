using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSS.DataAccess.Contexts;
using HSS.Domain.Models;
using HSS.Services.Abstractions;
using HSS.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace HSS.Services.Services
{
    public class ClinicService : IClinicService
    {
        private readonly ApplicationDbContext _context;

        public ClinicService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateClinic(CreateClinicDto dto)
        {
            if (dto == null) { 
                throw new ArgumentNullException(nameof(dto));
            }
            var clinic = new Clinic
            {
                AppointmentDurationInMinutes = dto.AppointmentDurationInMinutes,
                FinishAt = dto.FinishAt,
                HospitalId = dto.HospitalId,
                Location = dto.Location,
                MedicalDepartment = dto.MedicalDepartment,
                StartAt = dto.StartAt,
            };
            await _context.Clinics.AddAsync(clinic);
            return await _context.SaveChangesAsync()>0;  

        }

        public async Task<bool> DeleteClinic(int id)
        {
            if(id==null)
                throw new ArgumentNullException(nameof(id));
            var clinic = await  _context.Clinics.FindAsync(id);
            clinic.IsDeleted = true;
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<ClinicDto> GetClinic(int id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var result = await _context.Clinics.Where(x=>x.Id==id).Include(x=>x.Hospital).Select(x => new ClinicDto
            {
                Id = x.Id,
                AppointmentDurationInMinutes = x.AppointmentDurationInMinutes,
                FinishAt = x.FinishAt,
                HospitalId = x.HospitalId,
                HospitalName = x.Hospital.Name,
                Location = x.Location,
                MedicalDepartment = x.MedicalDepartment,
                StartAt = x.StartAt,
            }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<ClinicDto>> HospitalClinics(int id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var result = await _context.Clinics.Where(x => x.HospitalId == id).Include(x => x.Hospital).Select(x => new ClinicDto
            {
                Id = x.Id,
                AppointmentDurationInMinutes = x.AppointmentDurationInMinutes,
                FinishAt = x.FinishAt,
                HospitalId = x.HospitalId,
                HospitalName = x.Hospital.Name,
                Location = x.Location,
                MedicalDepartment = x.MedicalDepartment,
                StartAt = x.StartAt,
            }).ToListAsync();
            return result;  
        }

        public async Task<bool> UpdateClinic(int id, string Location,TimeSpan StartAt,TimeSpan FinshAt)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var result = await _context.Clinics.FindAsync(id);
            result.Location= Location;
            result.StartAt= StartAt;
            result.FinishAt= FinshAt;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
