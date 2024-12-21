using HSS.Domain;
using HSS.Presentation.MVC.Models;
using HSS.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Security.Claims;

namespace HSS.Presentation.MVC.Controllers
{
    [Authorize(Roles = RolesConstants.Receptionist)]
    public class ReceptionController
        (IReceptionServices _receptionServices, IToastNotification _toastNotification)
        : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId) || !Int32.TryParse(userId, out int userIdAsInt))
                    return RedirectToAction("UnAuthAccess", "Home");
                var specialization = await _receptionServices.GetSpecializationsByReceptionistIdAsync(userIdAsInt);
                return View(new ListSpecializationsViewModel(specialization));
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("حدث خطأ ما");
                _toastNotification.AddErrorToastMessage(ex.Message);
                return RedirectToAction("UnAuthAccess", "Home");
            }
        }

        public async Task<IActionResult> Clinics(int id)
        {
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId) || !Int32.TryParse(userId, out int userIdAsInt))
                    return RedirectToAction("UnAuthAccess", "Home");
                var clinics = await _receptionServices.GetClinicsByReceptionistIdAsync(userIdAsInt,id);
                return View(new ListClinicsViewModel(clinics));
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("حدث خطأ ما");
                return BadRequest(new
                {
                    ex.Message
                });
            }
        }

        public async Task<IActionResult> ClinicAppointments(int clinicId)
        {
            var appointments = await _receptionServices.AllClinicAppointments(clinicId);
            return View(new ClinicAppointmentsModelView(appointments, clinicId));
        }

        [HttpPost]
        public async Task<IActionResult> ClinicAppointmentsAsPartial(int clinicId)
        {
            var appointments = await _receptionServices.AllClinicAppointments(clinicId);
            return PartialView("/Components/_ClinicAppointments", new ClinicAppointmentsModelView(appointments, clinicId));
        }

        [HttpPost]
        public async Task<IActionResult> QueueAppointmentsAsPartial(int clinicId)
        {
            var appointments = await _receptionServices.ClinicAppointmentsQueue(clinicId);
            return PartialView("/Components/_QueueAppointments", new ClinicAppointmentsModelView(appointments, clinicId));
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
