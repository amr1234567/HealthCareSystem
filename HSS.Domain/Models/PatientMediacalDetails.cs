namespace HSS.Domain.Models
{
    public class PatientMediacalDetails : BaseClass<int>
    {
        [Required]
        [Range(0, 1, ErrorMessage = "Diabetes binary must be either 0 (no) or 1 (yes).")]
        public int DiabetesBinary { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "High blood pressure must be either 0 (no) or 1 (yes).")]
        public int HighBp { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "High cholesterol must be either 0 (no) or 1 (yes).")]
        public int HighChol { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Cholesterol check must be either 0 (no) or 1 (yes).")]
        public int CholCheck { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "BMI must be a positive value.")]
        public float Bmi { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Smoker must be either 0 (no) or 1 (yes).")]
        public int Smoker { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Stroke must be either 0 (no) or 1 (yes).")]
        public int Stroke { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Heart disease or attack must be either 0 (no) or 1 (yes).")]
        public int HeartDiseaseOrAttack { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Physical activity must be either 0 (no) or 1 (yes).")]
        public int PhysActivity { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Fruits intake must be either 0 (no) or 1 (yes).")]
        public int Fruits { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Vegetables intake must be either 0 (no) or 1 (yes).")]
        public int Veggies { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Heavy alcohol consumption must be either 0 (no) or 1 (yes).")]
        public int HvyAlcoholConsump { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Any healthcare must be either 0 (no) or 1 (yes).")]
        public int AnyHealthcare { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "No doctor due to cost must be either 0 (no) or 1 (yes).")]
        public int NoDocbcCost { get; set; }

        [Required]
        [Range(0, 5, ErrorMessage = "General health must be between 0 and 5.")]
        public int GenHlth { get; set; }

        [Required]
        [Range(0, 30, ErrorMessage = "Mental health score must be between 0 and 30.")]
        public int MentHlth { get; set; }

        [Required]
        [Range(0, 30, ErrorMessage = "Physical health score must be between 0 and 30.")]
        public int PhysHlth { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Difficulty walking must be either 0 (no) or 1 (yes).")]
        public int DiffWalk { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime LastVisitDate { get; set; }
    }
}
