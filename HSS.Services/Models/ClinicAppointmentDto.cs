using HSS.Domain.Models.Aggregates;

namespace HSS.Services.Models
{
    public class ClinicAppointmentDto
    {
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientNationalId { get; set; }

        public int ClinicId { get; set; }

        public string? ReasonForVisit { get; set; }

        public bool IsEnd { get; set; }
        public bool IsStart { get; set; }

        public ClinicAppointmentDto(ClinicAppointment appointment)
        {
            PatientId = appointment.PatientId;
            PatientName = appointment.Patient.Name;
            PatientNationalId = appointment.Patient.NationalId;
            ClinicId = appointment.ClinicId;
            ReasonForVisit = appointment.ReasonForVisit;
            IsEnd = appointment.IsEnd;
            IsStart = appointment.IsStarted;
        }
    }
}
