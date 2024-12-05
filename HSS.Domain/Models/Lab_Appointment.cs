namespace HSS.Domain.Models
{
    public class Lab_Appointment
    {
        public int id { get; set; }
        public int clinic_appointment_id { get; set; }
        public int lab_center_id { get; set; }
        public int hospital_id { get; set; }
        public string test_type { get; set; }
        public DateTime test_date { get; set; }
        public string test_notes { get; set; }
        public int lab_tester_id { get; set; }
        public string test_result { get; set; }
    }
}
