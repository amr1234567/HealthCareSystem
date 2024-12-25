using System.Security.Claims;
using HSS.Domain.Helpers;
using HSS.Presentation.MVC.Models;
using HSS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace HSS.Presentation.MVC.Controllers;

public class ClinicController(IReceptionServices _receptionServices, IToastNotification _toastNotification) : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        try
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId) || !Int32.TryParse(userId, out int userIdAsInt))
                return RedirectToAction("UnAuthAccess", "Home");
            var clinicId = GetClinicId();
            if (string.IsNullOrEmpty(clinicId) || !Int32.TryParse(clinicId, out int clinicIdAsInt))
                return RedirectToAction("UnAuthAccess", "Home");

            var specialization = await _receptionServices.ClinicAppointmentsQueue(clinicIdAsInt);
            return View(new ClinicAppointmentsModelView(specialization, clinicIdAsInt, true));
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage("حدث خطأ ما");
            _toastNotification.AddErrorToastMessage(ex.Message);
            return RedirectToAction("UnAuthAccess", "Home");
        }
    }

    public async Task<IActionResult> AppointmentDetails(int clinicId)
    {
        return View();
    }
    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    private string GetClinicId()
    {
        return User.FindFirstValue(CustomClaimType.ClinicId);
    }
}