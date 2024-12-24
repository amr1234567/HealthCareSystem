using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSS.Domain.ObjectValues;

namespace HSS.Domain.Dtos
{
    public class AppointmentPatientBaseData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
        public Gender Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public PatientAddress Location { get; set; }
        public string NationalId { get; set; }
    }
}
