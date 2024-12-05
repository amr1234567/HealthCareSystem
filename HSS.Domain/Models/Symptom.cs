namespace HSS.Domain.Models
{
    public class Symptom
    {
        public int id { get; set; }
        public string name { get; set; }
        public string age { get; set; }
        public string description { get; set; }
        public string severity { get; set; }
        public string duration { get; set; }
        public string type { get; set; }
        public string associated_conditions { get; set; }
        public string onset_pattern { get; set; }
        public bool is_chronic { get; set; }
        public bool treatment_required { get; set; }
    }
}
