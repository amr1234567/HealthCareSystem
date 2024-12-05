namespace HSS.Domain.Models
{
    public class EmergencyDepartment
    {
        public int id { get; set; }
        public int hospital_id { get; set; }
        public string location { get; set; }
        public int capacity { get; set; }
        public bool ambulance_availability { get; set; }
    }
}
