using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSS.Services.Enums;

namespace HSS.Services.SharedDto
{
    public class CreateAppointmentDto
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public TimeSpan Duration { get; set; }
        public MedicalDepartment medicalDepartment { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
