using HSS.Services.Models;

namespace HSS.Presentation.MVC.Models
{
    public class ListSpecializationsViewModel
    {
        public IEnumerable<SpecializationDto> Specializations { get; set; }
        public ListSpecializationsViewModel(IEnumerable<SpecializationDto> specializations)
        {
            Specializations = specializations;
        }
    }
}
