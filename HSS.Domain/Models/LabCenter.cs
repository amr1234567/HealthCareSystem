namespace HSS.Domain.Models
{
    public class LabCenter
    {
        public int id { get; set; }
        public int hospital_id { get; set; }
        public string location { get; set; }
        public string tests_available { get; set; }
        public string operational_hours { get; set; }
        public int appointment_duration { get; set; }
    }
}
