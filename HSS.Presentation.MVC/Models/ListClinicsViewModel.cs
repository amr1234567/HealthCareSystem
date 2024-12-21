using HSS.Services.Models;

namespace HSS.Presentation.MVC.Models
{
    public class ListClinicsViewModel
    {
        public IEnumerable<ClinicDto> Clinics;

        public ListClinicsViewModel(IEnumerable<ClinicDto> clinics)
        {
            Clinics = clinics;
        }
    }
}
