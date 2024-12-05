namespace HSS.Domain.Models
{
    public class ClinicAppointment
    {
        public int id { get; set; }
        public int patient_id { get; set; }
        public int clinic_id { get; set; }
        public int hospital_id { get; set; }
        public DateTime appointment_date { get; set; }
        public int doctor_id { get; set; }
        public int duration { get; set; }
        public string reason_for_visit { get; set; }
        public string notes { get; set; }
        public bool follow_up_needed { get; set; }
        public int clinic_appointment_id_related_to { get; set; }
        public bool lab_appoinment_needed { get; set; }
        public int lab_appoinments_number_done { get; set; }
        public bool radiology_appoinment_needed { get; set; }
        public int radiology_appoinments_number_done { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool is_end { get; set; }
    }
}
