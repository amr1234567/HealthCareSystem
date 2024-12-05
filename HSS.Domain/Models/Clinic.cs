﻿namespace HSS.Domain.Models
{
    public class Clinic
    {
        public int id { get; set; }
        public int hospital_id { get; set; }
        public string specialization { get; set; }
        public string location { get; set; }
        public string operational_hours { get; set; }
        public int appointment_duration { get; set; }
    }
}
