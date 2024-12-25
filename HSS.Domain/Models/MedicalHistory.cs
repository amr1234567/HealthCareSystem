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
        [DataType(DataType.DateTime)]
        public DateTime DiagnosisDate { get; set; }

        [StringLength(1000, ErrorMessage = "Treatment details cannot exceed 1000 characters.")]
        public string Treatment { get; set; } //??? 

        [AllowNull]
        [DataType(DataType.DateTime)]
        public DateTime? ExpectedTimeForTreatment { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [AllowNull]
        [StringLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string Notes { get; set; }
    }

}
