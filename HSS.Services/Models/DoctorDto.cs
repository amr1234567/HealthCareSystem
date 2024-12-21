using HSS.Domain.IdentityModels;

namespace HSS.Services.Models
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HireDate { get; set; }

        public TimeSpan WorkingTime { get; set; }

        public TimeSpan StartAt { get; set; }

        public int HospitalId { get; set; }

        public string Specialization { get; set; }

        public int ClinicId { get; set; }


        public DoctorDto(Doctor doctor)
        {
            Id = doctor.Id;
            Name = doctor.Name;
            StartAt = doctor.StartAt;
            WorkingTime = doctor.WorkingTime;
            HireDate = doctor.HireDate;
            HospitalId = doctor.HospitalId;
            Specialization = doctor.Specialization;
            ClinicId = doctor.ClinicId;
        }
    }
}
