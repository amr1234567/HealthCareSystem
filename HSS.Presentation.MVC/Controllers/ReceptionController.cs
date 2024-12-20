using HSS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HSS.Presentation.MVC.Controllers
{
    [Authorize(Roles = RolesConstants.Receptionist)]
    public class ReceptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
