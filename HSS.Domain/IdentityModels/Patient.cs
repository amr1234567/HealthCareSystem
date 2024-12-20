﻿using HSS.Domain.Models;
using HSS.Domain.ObjectValues;

namespace HSS.Domain.IdentityModels
{
    public class Patient : IdentityUser
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Sex must be either 0 (female) or 1 (male).")]
        public Gender Sex { get; set; }

        [AllowNull]
        public AgeGroup? AgeCategory { get; set; }

        [AllowNull]
        public EducationLevel? EducationLevel { get; set; }

        [AllowNull]
        public IncomeCategory? IncomeCategory { get; set; }

        [Required]
        public PatientAddress Address { get; set; }

        [AllowNull]
        public int? PatientMediacalDetailsId { get; set; }

        [AllowNull]
        public PatientMediacalDetails PatientMediacalDetails { get; set; }
    }
}
