using System.Security.Claims;
using HSS.Domain.Helpers;
using HSS.Domain.Models;
using HSS.Presentation.MVC.Models;
using HSS.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace HSS.Presentation.MVC.Controllers;

public class ClinicController( IToastNotification _toastNotification, IClinicService clinicService) : Controller
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

            var specialization = await clinicService.ClinicAppointments(clinicIdAsInt);
            return View(new ClinicAppointmentsModelView(specialization, clinicIdAsInt, true));
        }
        catch (Exception ex)
        {
            _toastNotification.AddErrorToastMessage("حدث خطأ ما");
            _toastNotification.AddErrorToastMessage(ex.Message);
            return RedirectToAction("UnAuthAccess", "Home");
        }
    }

    public async Task<IActionResult> AppointmentDetails([FromQuery]int appointmentId)
    {
        var appointment = await clinicService.GetAppointmentPatientBaseData(appointmentId);
        return View(new AppointmentDetailsViewModel(appointment, appointmentId));
    }

    [HttpPost]
    public async Task<IActionResult> FinishAppointment(int appointmentId)
    {
        try{
            var result = await clinicService.AppointmentFinished(appointmentId);
            if (result)
                return Json(new { success = result });
            return Json(new { success = result, message = "هناك خطأ ما" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> GetMedicines(string query = "")
    {
        try
        {
            var result = await clinicService.Medicines(query);
            return Json(new { success = true, medicines = result });
        }catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    public async Task<IActionResult> MakePrescriptions([FromQuery] int appointmentId)
    {
        var model = new MakePrescriptionsViewModel(appointmentId);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitPrescriptions([FromBody]SubmitPrescriptionsModel model)
    {
        return Ok();
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

public class MakePrescriptionsViewModel
{
    public int AppointmentId { set; get; }
    public MakePrescriptionsViewModel(int appointmentId)
    {
        AppointmentId = appointmentId;
    }
}
