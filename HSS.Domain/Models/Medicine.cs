namespace HSS.Domain.Models
{
    public class Medicine
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string manufacturer { get; set; }
        public string active_ingredient { get; set; }
        public string dosage { get; set; }
        public DateTime expiration_date { get; set; }
        public DateTime approval_date { get; set; }
        public string storage_conditions { get; set; }
        public bool prescription_required { get; set; }
        public float cost { get; set; }
        public string usage_instructions { get; set; }
    }
}
