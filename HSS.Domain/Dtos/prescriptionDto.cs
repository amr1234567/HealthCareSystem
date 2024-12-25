using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.Dtos
{
    public class prescriptionDto
    {
        public int Id { get; set; }
        public int NumberOfUnits { get; set; }
        public int DispensedAmount { get; set; }
        public DispenseStatus DispenseStatus { get; set; } = DispenseStatus.NotDispensed;
        public int TimesOfDispensed { get; set; } = 0;
        public string TimingDescription { get; set; }
        public DateTime DispensedDate { get; set; }
        public MedicineUnitType MedicineUnitType { get; set; }
        public DosageFrequency DosageFrequency { get; set; }
    }
}
