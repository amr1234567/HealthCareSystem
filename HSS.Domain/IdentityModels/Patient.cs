using HSS.Domain.Models;
using HSS.Domain.ObjectValues;

namespace HSS.Domain.IdentityModels
{
    public class Patient : IdentityUser<int>
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Sex must be either 0 (female) or 1 (male).")]
        public SexType Sex { get; set; }

        [Required]
        public AgeGroup AgeCategory { get; set; }

        [Required]
        public EducationLevel EducationLevel { get; set; }

        [Required]
        public IncomeCategory IncomeCategory { get; set; }

        [Required]
        public PatientAddress Address { get; set; }

        [Required]
        public int PatientMediacalDetailsId { get; set; }
        public PatientMediacalDetails PatientMediacalDetails { get; set; }
    }
}
