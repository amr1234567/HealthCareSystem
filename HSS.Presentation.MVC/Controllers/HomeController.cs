using HSS.Domain;
using HSS.Domain.BaseModels;
using HSS.Domain.Enums;
using HSS.Domain.Models;
using HSS.Presentation.MVC.Models;
using HSS.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Diagnostics;
using System.Security.Claims;

namespace HSS.Presentation.MVC.Controllers
{
    public class HomeController(ILogger<HomeController> logger,
        IToastNotification _toastNotification, 
        IUserIdentityServices<IdentityUser> identityServices) : Controller
    {
       
        public IActionResult UnAuthAccess()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
                return CheckRole();
            else
                _toastNotification.AddErrorToastMessage("انت غير مسجل بالنظام");

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
                var result = await identityServices.LoginWithCookie(model.NationalId, model.Password, true);
                _toastNotification.AddSuccessToastMessage("تم تسجيل الدخول بنجاح");

                //if(string.IsNullOrEmpty(returnUrl))
                    //return LocalRedirect(Url.GetLocalUrl(returnUrl));
                return CheckRole(result);
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("حدث خطأ ما");
                _toastNotification.AddErrorToastMessage(ex.Message);
                return View(model);
            }
        }

        private IActionResult CheckRole(List<Role>? userRoles = null)
        {
            var roles = new List<ApplicationRole>();
            if (userRoles != null)
            {
                roles = userRoles.Select(r => r.RoleName).ToList();
            }
            else
            {
                roles = User.Claims.Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => (ApplicationRole)Enum.Parse(typeof(ApplicationRole), c.Value)).ToList();
            }
            if (!roles.Any())
                return RedirectToAction(nameof(Logout));

            if (roles.Contains(ApplicationRole.Receptionist))
                return RedirectToAction("Index", "Reception");
            if (roles.Contains(ApplicationRole.Doctor))
                return RedirectToAction("Index", "Clinic");

            // Add more role checks as needed
            // if (roles.Contains(ApplicationRole.Doctor.ToString()))
            //     return RedirectToAction("Index", "Doctor");

            return RedirectToAction(nameof(Logout));
        }

        public async Task<IActionResult> Logout()
        {
            await identityServices.LogOutInMvc();
            return RedirectToAction(nameof(Login));
        }
    }
}
