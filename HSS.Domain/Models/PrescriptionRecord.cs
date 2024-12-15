using HSS.Domain.Models.Aggregates;

namespace HSS.Domain.Models
{
    public class PrescriptionRecord : BaseClass<int>
    {
        public Medicine Medicine { get; set; }
        [Required]
        public int MedicineId { get; set; }

        public string MedicineName { get; set; }

        [Required, EnumDataType(typeof(MedicineUnitType))]
        public MedicineUnitType MedicineUnitType { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int NumberOfUnits { get; set; }

        [Required]
        public DosageFrequency DosageFrequency { get; set; }

        [AllowNull, MinLength(5)]
        public string TimingDescription { get; set; }

        [Required, EnumDataType(typeof(DispenseStatus))]
        public DispenseStatus DispenseStatus { get; set; } = DispenseStatus.NotDispensed;

        [AllowNull, DataType(DataType.DateTime)]
        public DateTime DispensedDate { get; set; }

        [AllowNull, Range(0, int.MaxValue)]
        public int DispensedAmount { get; set; }

        public ClinicAppointment ClinicAppointment { get; set; }
        public int ClinicAppointmentId { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int TimesOfDispensed { get; set; } = 0; // ديه انا عايز اقول عدد المرات اللي راح فيها للصيدلية عشان يصرف من الدواء ده لحد ما خلص الكمية اللي الدكتور قال عليها
    }

}
