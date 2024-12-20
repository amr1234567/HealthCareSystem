using HSS.Presentation.MVC.Models;
using HSS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Diagnostics;

namespace HSS.Presentation.MVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger, IToastNotification _toastNotification, IUserIdentityServices identityServices) : Controller
    {

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _toastNotification.AddErrorToastMessage("الرجاء التأكد من صحة البيانات");
                    return View(model);
                }
                var result = await identityServices.Login(model.NationalId, model.Password);
                if (result == null)
                {
                    _toastNotification.AddErrorToastMessage("الرجاء التأكد من صحة البيانات");
                    return View(model);
                }
                _toastNotification.AddSuccessToastMessage("تم تسجيل الدخول بنجاح");
                return RedirectToAction(nameof(Privacy));
            }
            catch 
            {
                _toastNotification.AddErrorToastMessage("حدث خطأ ما");
                return View(model);
            }
        }
    }
}
