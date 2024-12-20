using HSS.Domain.Models;

namespace HSS.Services.Models
{
    public class ReceptionDto
    {
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }

        public TimeSpan StartAt { get; set; }

        public TimeSpan EndAt { get; set; }


        public ReceptionDto(Reception reception)
        {
            HospitalId = reception.HospitalId;
            HospitalName = reception.Hospital.Name;
            StartAt = reception.StartAt;
            EndAt = reception.EndAt;
        }
    }
}
