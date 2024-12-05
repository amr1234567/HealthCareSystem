namespace HSS.Domain.Models
{
    public class Reception
    {
        public int id { get; set; }
        public int hospital_id { get; set; }
        public string location { get; set; }
        public string working_hours { get; set; }
        public string contact_email { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
