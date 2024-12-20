using HSS.Domain.IdentityModels;

namespace HSS.Domain.Models.Aggregates
{
    public class ClinicAppointment : Appointment
    {
        [Required]  // Ensures this field cannot be null
        [Range(1, int.MaxValue, ErrorMessage = "PatientId must be a positive integer.")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]  // Ensures this field cannot be null
        [Range(1, int.MaxValue, ErrorMessage = "ClinicId must be a positive integer.")]
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        [AllowNull]  // Ensures this field cannot be null
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [Required]  // Ensures this field cannot be null
        [StringLength(500, ErrorMessage = "Reason for visit cannot exceed 500 characters.")]
        public string ReasonForVisit { get; set; }

        [Required,Range(0, int.MaxValue, ErrorMessage = "Lab appointments number done must be a non-negative integer.")]
        public int LabAppointmentsNumberDone { get; set; } = 0;

        [Required,Range(0, int.MaxValue, ErrorMessage = "Radiology appointments number done must be a non-negative integer.")]
        public int RadiologyAppointmentsNumberDone { get; set; }

        [AllowNull]
        public Treatment Treatment { get; set; }

        [Required]  // Ensures this field cannot be null
        public bool FollowUpNeeded { get; set; }

        [AllowNull]
        [DataType(DataType.Time)]
        public TimeSpan? FollowUpExpectedPeriod { get; set; }

        [Required]  // Ensures this field is mandatory
        public bool RadiologyAppointmentNeeded { get; set; }

        [Required]  // Ensures this field is mandatory
        public bool LabAppointmentNeeded { get; set; }

        [AllowNull]
        public List<RadiologyAppointment> RadiologyAppointments { get; set; }

        [AllowNull]
        public List<LabAppointment> LabAppointments { get; set; }

        [AllowNull]
        public int ClinicAppointmentIdRelatedTo { get; set; }
        public ClinicAppointment? ClinicAppointmentRelatedTo { get; set; }

        public bool IsStarted { get; set; } = false;

        public bool IsEnd
        {
            get
            {
                var needMore = !FollowUpNeeded &&
                    !RadiologyAppointmentNeeded &&
                    !LabAppointmentNeeded;
                var rangeTime = CreatedAt.Add(FollowUpExpectedPeriod ?? TimeSpan.MinValue).AddDays(4);
                var timeOut = FollowUpExpectedPeriod != null && DateTime.UtcNow < rangeTime;
                return needMore || timeOut;
            }
        } 
    }


}
