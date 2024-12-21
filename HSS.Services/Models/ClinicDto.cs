using HSS.Domain.Models;

namespace HSS.Services.Models
{
    public class ClinicDto
    {
        public int ClinicId { get; set; }
        public string ClinicName { set; get; }
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }

        public string DoctorName { set; get; }

        public string SpecializationName { get; set; }
        public string SpecializationIcon { get; set; }

        public TimeSpan StartAt { get; set; }

        public bool IsOpen { set; get; }

        public TimeSpan FinishAt { get; set; }

        public int AppointmentDurationInMinutes { get; set; }

        public ClinicDto(Clinic clinic, string doctorName)
        {
            ClinicName = clinic.Name;
            ClinicId = clinic.Id;
            HospitalId = clinic.HospitalId;
            HospitalName = clinic.Hospital.Name;
            SpecializationName = clinic.Specialization.Name;
            StartAt = clinic.StartAt;
            FinishAt = clinic.FinishAt;
            AppointmentDurationInMinutes = clinic.AppointmentDurationInMinutes;
            DoctorName = doctorName;
            SpecializationIcon = clinic.Specialization.Icon;
        }

        public ClinicDto(Clinic clinic)
        {
            ClinicName = clinic.Name;
            ClinicId = clinic.Id;
            HospitalId = clinic.HospitalId;
            HospitalName = clinic.Hospital.Name;
            SpecializationName = clinic.Specialization.Name;
            StartAt = clinic.StartAt;
            FinishAt = clinic.FinishAt;
            AppointmentDurationInMinutes = clinic.AppointmentDurationInMinutes;
            DoctorName = string.Empty;
        }
    }
}
