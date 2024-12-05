namespace HSS.Domain.Models
{
    public class Pharmacy
    {
        public int id { get; set; }
        public int hospital_id { get; set; }
        public string location { get; set; }
        public string working_hours { get; set; }
        public bool emergency_availability { get; set; }
        public string contact_number { get; set; }
        public string services_provided { get; set; }
        public string pharmacist_in_charge { get; set; }
        public bool availability_of_specialized_medicines { get; set; }
    }
}
