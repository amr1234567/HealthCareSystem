using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HSS.Services.Enums;

namespace HSS.Services.Dtos
{
    public class CreateClinicDto
    {
        [Required]
        public int HospitalId { get; set; }
        [Required]
        public string Location { get; set; } // وصف للمكان بالمستشفي
        [Required]
        public MedicalDepartment MedicalDepartment { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartAt { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan FinishAt { get; set; }

        [Required, Range(5, 90)]
        public int AppointmentDurationInMinutes { get; set; }
    }
}
