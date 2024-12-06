using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Models.Aggregates
{
    public class Appointment : BaseClass<int>
    {
        [Required]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Duration { get; set; }

        [AllowNull, MaxLength(600)]
        public string Notes { get; set; }

        [Required]  // Ensures this field is mandatory
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        [Required]  // Ensures this field is mandatory
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [Required]  // Ensures this field is mandatory
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        [AllowNull]
        [Range(0, int.MaxValue, ErrorMessage = "Clinic appointment ID related to must be a non-negative integer.")]
        public int ClinicAppointmentIdRelatedTo { get; set; }
        public ClinicAppointment? ClinicAppointmentRelatedTo { get; set; }

    }
}
