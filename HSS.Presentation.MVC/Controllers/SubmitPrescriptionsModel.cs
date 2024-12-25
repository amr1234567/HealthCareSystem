namespace HSS.Presentation.MVC.Controllers
{
    public class SubmitPrescriptionsModel
    {
        public List<PrescriptionModel> Prescriptions { get; set; }
        public int AppointmentId { get; set; }
    }

    public class PrescriptionModel
    {
        public int MedicineId { get; set; }
        public int NumberOfUnits { get; set; }
        public int DosageFrequency { get; set; }
        public string? TimingDescription {  get; set; }
    }
}