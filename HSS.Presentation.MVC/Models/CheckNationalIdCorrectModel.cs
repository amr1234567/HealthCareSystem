using System.ComponentModel.DataAnnotations;

namespace HSS.Presentation.MVC.Controllers;

public class CheckNationalIdCorrectModel
{
    [Required]
    public string NationalId { set; get; }
}