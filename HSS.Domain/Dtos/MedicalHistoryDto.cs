using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Dtos
{
    public class MedicalHistoryDto
    {
        public string Diagnosis { get; set; }
        public DateTime DiagnosisDate { get; set; }

        [StringLength(1000, ErrorMessage = "Treatment details cannot exceed 1000 characters.")]
        public string Treatment { get; set; } //??? 

        [AllowNull]
        [DataType(DataType.DateTime)]
        public DateTime? ExpectedTimeForTreatment { get; set; }

        [AllowNull]
        [StringLength(2000, ErrorMessage = "Notes cannot exceed 2000 characters.")]
        public string Notes { get; set; }
    }
}
