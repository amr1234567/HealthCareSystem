namespace HSS.Domain.Models
{
    public class MedicalHistory
    {
        public int id { get; set; }
        public int patient_id { get; set; }
        public string diagnosis { get; set; }
        public DateTime diagnosis_date { get; set; }
        public string treatment { get; set; }
        public DateTime treatment_start_date { get; set; }
        public DateTime treatment_end_date { get; set; }
        public string prescribed_medicines { get; set; }
        public string surgery { get; set; }
        public DateTime surgery_date { get; set; }
        public bool follow_up_needed { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string notes { get; set; }
    }
}
