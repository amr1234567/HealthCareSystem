using HSS.Domain;
using HSS.Domain.Abstractions;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models.Aggregates;
using HSS.Presentation.MVC.Models;
using HSS.Services.Abstractions;
using HSS.Services.SharedDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HSS.Presentation.MVC.Controllers
{
    [Authorize(Roles = RolesConstants.Receptionist)]
    public class ReceptionController
        (IReceptionServices _receptionServices, IToastNotification _toastNotification, IUserIdentityRepository userIdentityRepository)
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
            return View(new ClinicAppointmentsModelView(appointments, clinicId, false));
        }

        [HttpPost]
        public async Task<IActionResult> ClinicAppointmentsAsPartial(int clinicId)
        {
            var appointments = await _receptionServices.AllClinicAppointments(clinicId);
            return PartialView("~/Views/Reception/Components/_AppointmentsList.cshtml",
                new ClinicAppointmentsModelView(appointments, clinicId, false));
        }

        [HttpPost]
        public async Task<IActionResult> QueueAppointmentsAsPartial(int clinicId)
        {
            var appointments = await _receptionServices.ClinicAppointmentsQueue(clinicId);
            return PartialView("~/Views/Reception/Components/_AppointmentsList.cshtml",
                new ClinicAppointmentsModelView(appointments, clinicId, true));
        }

        [HttpPost]
        public async Task<IActionResult> DelayAppointment(DelayAppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "الرجاء التأكد من صحة البيانات" });
            }
            try
            {
                var dateTime = DateTime.Parse($"{model.NewDate} {model.NewTime}");
                var result = await _receptionServices.DelayAppointment(model.PatientNationalId, dateTime);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetAvailableTimeSlots(int clinicId, string date)
        {
            try
            {
                if (DateTime.TryParse(date, out DateTime selectedDate))
                {
                    var timeSlots = await _receptionServices.GetAvailableTimeSlots(clinicId, selectedDate);
                    return Json(new { success = true, timeSlots });
                }
                return Json(new { success = false, message = "تاريخ غير صالح" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "الرجاء التأكد من صحة البيانات" });
            }
            try
            {
                var result = await _receptionServices.CreateAppointment(dto);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CheckNationalIdCorrect([FromBody]CheckNationalIdCorrectModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _toastNotification.AddErrorToastMessage("الرجاء التأكد من صحة البيانات");
                    return Json(new { success = false, message = "الرجاء التأكد من صحة البيانات" });
                }
                var result = await userIdentityRepository.GetIdentityUserByNationalId(model.NationalId);
                if (result == null || result is not Patient)
                    return Json(new { success = false, message = "لا يوجد مريض بهذا المعرف" });
                return Json(new { success = true, name = result.Name });
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("الرجاء التأكد من صحة البيانات");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmAppointment([FromBody] EditAppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("الرجاء التأكد من صحة البيانات");
                return Json(new { success = false, message = "الرجاء التأكد من صحة البيانات" });
            }
            var result = await _receptionServices.ConfirmAppointment(model.AppointmentId);

            return PartialView("~/Views/Reception/Components/_AppointmentItem.cshtml",
                new AppointmentItemViewModel(result, model.IsQueue));
        }

        [HttpPost]
        public async Task<IActionResult> CancelAppointment([FromBody] EditAppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("الرجاء التأكد من صحة البيانات");
                return Json(new { success = false, message = "الرجاء التأكد من صحة البيانات" });
            }
            var result = await _receptionServices.CancelAppointment(model.AppointmentId);

            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAppointmentFromQueue([FromBody] EditAppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("الرجاء التأكد من صحة البيانات");
                return Json(new { success = false, message = "الرجاء التأكد من صحة البيانات" });
            }
            var result = await _receptionServices.RemoveFromQueue(model.AppointmentId);
            return Json(new { success = result });
        }

        [HttpPost]
        public async Task<IActionResult> StartAppointment([FromBody] EditAppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                _toastNotification.AddErrorToastMessage("الرجاء التأكد من صحة البيانات");
                return Json(new { success = false, message = "الرجاء التأكد من صحة البيانات" });
            }
            var result = await _receptionServices.StartAppointment(model.AppointmentId);
            return PartialView("~/Views/Reception/Components/_AppointmentItem.cshtml",
                new AppointmentItemViewModel(result, model.IsQueue));
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }

    public class AppointmentItemViewModel(AppointmentDto appointment, bool isQueue = false)
    {
        public bool IsQueueItem { set; get; } = isQueue;
        public AppointmentDto Appointment { set; get; } = appointment;
    }

    public class EditAppointmentModel
    {
        [Required]
        public int AppointmentId { get; set; }

        public bool IsQueue { get; set; } = false;
    }

    public class CheckNationalIdCorrectModel
    {
        [Required]
        public string NationalId { set; get; }
    }
}
