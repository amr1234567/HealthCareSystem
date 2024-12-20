﻿
namespace HSS.Services.SharedDto
{
    public class CreateAppointmentDto
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string NationalId { get; set; }
        public int ClinicId { get; set; }
    }
}
