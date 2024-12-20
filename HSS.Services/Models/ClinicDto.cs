using HSS.Domain.Models;

namespace HSS.Services.Models
{
    public class ClinicDto
    {
        public int ClinicId { get; set; }
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }

        public string SpecializationName { get; set; }

        public TimeSpan StartAt { get; set; }

        public TimeSpan FinishAt { get; set; }

        public int AppointmentDurationInMinutes { get; set; }

        public ClinicDto(Clinic clinic)
        {
            ClinicId = clinic.Id;
            HospitalId = clinic.HospitalId;
            HospitalName = clinic.Hospital.Name;
            SpecializationName = clinic.Specialization.Name;
            StartAt = clinic.StartAt;
            FinishAt = clinic.FinishAt;
            AppointmentDurationInMinutes = clinic.AppointmentDurationInMinutes;
        }
    }
}
