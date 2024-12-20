using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.SharedDto
{
    public class AppointmentDto
    {
        public string ClinicName { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string NationalId { get; set; }
        public DateTime Reserved {  get; set; }
        public string Type { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan EndAt { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
