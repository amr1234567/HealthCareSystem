namespace HSS.Domain.Models
{
    using HSS.Domain.IdentityModels;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MedicalHistory : BaseClass<int>
    {
        [Required]
        public int PatientId { get; set; } // Reference to the associated Patient
        public Patient Patient { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Diagnosis cannot exceed 500 characters.")]
        public string Diagnosis { get; set; }

        [Required]
        public DateTime DiagnosisDate { get; set; }

        [StringLength(1000, ErrorMessage = "Treatment details cannot exceed 1000 characters.")]
        public string Treatment { get; set; } //???

        [AllowNull]
        public DateTime? TreatmentStartDate { get; set; }

        [AllowNull]
        public DateTime? TreatmentEndDate { get; set; }

        public bool FollowUpNeeded { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [AllowNull]
        [StringLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string Notes { get; set; }
    }

}
