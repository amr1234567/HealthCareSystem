﻿using HSS.Domain.IdentityModels;

namespace HSS.Domain.Models.Aggregates
{
    public class LabAppointment : Appointment
    {
        [Required]  // Ensures this field cannot be null
        [Range(1, int.MaxValue, ErrorMessage = "LabCenterId must be a positive integer.")]
        public int LabCenterId { get; set; }
        public LabCenter LabCenter { get; set; }

        [Required]  // Ensures this field cannot be null
        public int TestTypeId { get; set; }
        public LabCenterTest TestType { get; set; }

        [AllowNull]
        [DataType(DataType.DateTime)]
        public DateTime? ResultDate { get; set; }

        [Required]  // Ensures this field cannot be null
        [Range(1, int.MaxValue, ErrorMessage = "LabTesterId must be a positive integer.")]
        public int LabTesterId { get; set; }
        public LabManager LabManager { get; set; }

        [StringLength(500, ErrorMessage = "Test result cannot exceed 500 characters.")]
        public string TestResult { get; set; }

        [AllowNull]
        public int ClinicAppointmentIdRelatedTo { get; set; }
        public ClinicAppointment? ClinicAppointmentRelatedTo { get; set; }


        public bool IsFinish => ResultDate != null;
    }

}
