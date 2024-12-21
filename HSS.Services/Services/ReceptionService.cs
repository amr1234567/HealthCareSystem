using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSS.DataAccess.Contexts;
using HSS.Domain.Models.Aggregates;
using HSS.Services.Abstractions;
using HSS.Services.SharedDto;
using Microsoft.EntityFrameworkCore;

namespace HSS.Services.Services
{
    public class ReceptionService : IReceptionService
    {
        private readonly ApplicationDbContext _context;

        public ReceptionService(ApplicationDbContext context)
        {
            _context = context;
        }


        public Task<bool> AddToQueue(int AppointmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateAppointment(CreateAppointmentDto dto)
        {
            var appointment = new Appointment
            {
                AppointmentDate = dto.AppointmentDate,
                CreatedAt = dto.CreatedAt,
                Duration = dto.Duration,
                HospitalId = dto.HospitalId,
                Notes = dto.Notes,               
            };
            await _context.Appointments.AddAsync(appointment);
            return await _context.SaveChangesAsync()>0;
        }

        public Task<bool> DelayAppointment(DateTime time, int appointmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveAppointment(int id)
        {
            return await _context.Appointments.Where(x => x.Id == id).ExecuteDeleteAsync()>0;
        }
    }
}
