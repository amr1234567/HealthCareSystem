using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HSS.Presentation.MVC.Models
{
    public class LoginViewModel
    {
        [DisplayName("الرقم القومي")]
        [Required(ErrorMessage = "{0} مطلوب")]
        [RegularExpression(@"\d{14}", ErrorMessage = "{0} يجب أن يحتوي على 14 رقمًا")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "{0} يجب أن يكون 14 رقمًا بالضبط")]
        public string NationalId { get; set; } // Changed from bool to string

        [DisplayName("كلمة المرور")]
        [Required(ErrorMessage = "{0} مطلوبة")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }


}
