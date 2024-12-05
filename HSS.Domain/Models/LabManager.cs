namespace HSS.Domain.Models
{
    public class LabManager
    {
        public int id { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string contact_number { get; set; }
        public string email { get; set; }
        public DateTime hire_date { get; set; }
        public int experience_years { get; set; }
        public string certifications { get; set; }
        public int lab_center_id { get; set; }
    }
}
