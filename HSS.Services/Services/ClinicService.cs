using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSS.DataAccess.Contexts;
using HSS.Domain.Dtos;
using HSS.Domain.Models;
using HSS.Domain.Models.Aggregates;
using HSS.Services.Abstractions;
using HSS.Services.Dtos;
using HSS.Services.SharedDto;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<List<AppointmentDto>> ClinicAppointments(int doctorId)
        {
            var doctor = await _context.Doctors
                .Include(x => x.Clinic)
                .FirstOrDefaultAsync(x => x.Id == doctorId);

            var appontments = await _context.ClinicAppointment
                .Where(x => x.AppointmentDate > DateTime.Now)
                .Where(x => x.ClinicId == doctor.ClinicId && (x.AppointmentDate.TimeOfDay < doctor.StartAt + doctor.WorkingTime && x.AppointmentDate.TimeOfDay >= doctor.StartAt))
                .Select(x => new AppointmentDto(x))
                .ToListAsync();
            return appontments;
        }

        public async Task<bool> AppointmentFinished(int appointmentId)
        {
            await _context.ClinicAppointment.Where(x => x.Id == appointmentId).ExecuteUpdateAsync(x => x.SetProperty(x => x.IsEnd, true));
            return true;
        }

        public async Task<bool> CreateClinic(CreateClinicDto dto)
        {
            if (dto == null)
            {
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
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> DeleteClinic(int id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var clinic = await _context.Clinics.FindAsync(id);
            clinic.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ClinicDto> GetClinic(int id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var result = await _context.Clinics.Where(x => x.Id == id).Include(x => x.Hospital).Select(x => new ClinicDto
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

        public async Task<bool> UpdateClinic(int id, string Location, TimeSpan StartAt, TimeSpan FinshAt)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            var result = await _context.Clinics.FindAsync(id);
            result.Location = Location;
            result.StartAt = StartAt;
            result.FinishAt = FinshAt;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AppointmentPatientBaseData> GetAppointmentPatientBaseData(string NationalId)
        {
            var patientData = await _context.Patients.FindAsync(NationalId);
            return new AppointmentPatientBaseData
            {
                age = (int)(DateTime.Now - patientData.DateOfBirth).TotalDays / 365,
                Gender = patientData.Sex,
                Id = patientData.Id,
                Location = patientData.Address,
                Name = patientData.Name,
                NationalId = NationalId,
                PhoneNumber = patientData.ContactNumber
            };
        }

        public async Task<bool> AddRadiologyAppointment(int appointmentId, int radioId)
        {
            var appointment = await _context.ClinicAppointment.FindAsync(appointmentId);
            appointment.RadiologyAppointmentNeeded = true;
            var radioAppoint = new RadiologyAppointment
            {
                ClinicAppointmentIdRelatedTo = appointmentId,
                RadiologyTesterId = radioId,

            };
            appointment.RadiologyAppointments.Add(radioAppoint);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddTestAppointment(int appointmentId, int testId)
        {
            var appointment = await _context.ClinicAppointment.FindAsync(appointmentId);
            appointment.LabAppointmentNeeded = true;
            var labappo = new LabAppointment
            {
                ClinicAppointmentIdRelatedTo = appointmentId,
                TestTypeId = testId,
            };
            appointment.LabAppointments.Add(labappo);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<SelectListItem>> Medicines(string? medicine)
        {
            var medicines = _context.Medicines.AsQueryable();
            if (!string.IsNullOrEmpty(medicine))
            {
                medicines.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{medicine.ToLower()}%"));
            }
            var returnmed = medicines.ToList();
            var listmed = new List<SelectListItem>();
            foreach (var item in returnmed)
            {
                listmed.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            return listmed;
        }
        public async Task<List<SelectListItem>> RadiologyTypes(string? type)
        {
            var radiologyTypes = _context.radiologyTestTypes.AsQueryable();
            if (!string.IsNullOrEmpty(type))
            {
                radiologyTypes.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{type.ToLower()}%"));
            }
            var returntypes = radiologyTypes.ToList();
            var listtypes = new List<SelectListItem>();
            foreach (var item in returntypes)
            {
                listtypes.Add(new SelectListItem
                {
                    Text= item.Name,    
                    Value = item.Id.ToString()
                });
            }
            return listtypes;
        }
        public async Task<List<SelectListItem>> TestTypes(string? type)
        {
            var testTypes = _context.radiologyTestTypes.AsQueryable();
            if (!string.IsNullOrEmpty(type))
            {
                testTypes.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{type.ToLower()}%"));
            }
            var returntypes = testTypes.ToList();
            var listtypes = new List<SelectListItem>();
            foreach (var item in returntypes)
            {
                listtypes.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            return listtypes;
        }
    }
}