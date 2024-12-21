using HSS.Domain;
using HSS.Domain.BaseModels;
using HSS.Domain.Enums;
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
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _toastNotification.AddErrorToastMessage("الرجاء التأكد من صحة البيانات");
                    return View(model);
                }
                await identityServices.LoginWithCookie(model.NationalId, model.Password, true);
                _toastNotification.AddSuccessToastMessage("تم تسجيل الدخول بنجاح");

                //if(string.IsNullOrEmpty(returnUrl))
                    //return LocalRedirect(Url.GetLocalUrl(returnUrl));
                return CheckRole();
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("حدث خطأ ما");
                _toastNotification.AddErrorToastMessage(ex.Message);
                return View(model);
            }
        }

        private IActionResult CheckRole()
        {

            if (User.IsInRole(RolesConstants.Receptionist))
                return RedirectToAction("Index", "Reception");
            return RedirectToAction(nameof(Logout));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await identityServices.LogOutInMvc();
            return RedirectToAction(nameof(Login));
        }
    }
}
