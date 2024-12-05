namespace HSS.Domain.Models
{
    public class RadiologyManager
    {
        public int id { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string contact_number { get; set; }
        public string email { get; set; }
        public DateTime hire_date { get; set; }
        public int experience_years { get; set; }
        public string certification { get; set; }
        public int radiology_center_id { get; set; }
    }
}
