namespace HSS.Domain.Models
{
    public class Hospital
    {
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public int capacity { get; set; }
        public string contact_number { get; set; }
        public string email { get; set; }
        public DateTime established_date { get; set; }
        public int admin_id { get; set; }
        public string facilities { get; set; }
        public string specialties { get; set; }
        public string working_hours { get; set; }
        public bool emergency_services { get; set; }
        public int bed_availability { get; set; }
        public int number_of_doctors { get; set; }
        public int number_of_nurses { get; set; }
        public int departments_count { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string website_url { get; set; }
        public string license_number { get; set; }
        public string tax_identification_number { get; set; }
        public float rating { get; set; }
        public int review_count { get; set; }
        public string feedback_url { get; set; }
        public bool is_automated { get; set; }
        public bool has_online_consultation { get; set; }
    }
}
