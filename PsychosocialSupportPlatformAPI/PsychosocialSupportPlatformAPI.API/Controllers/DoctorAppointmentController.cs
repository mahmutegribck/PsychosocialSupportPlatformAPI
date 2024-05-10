using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorAppointmentController : ControllerBase
    {
        private readonly IAppointmentScheduleService _appointmentScheduleService;

        public DoctorAppointmentController(IAppointmentScheduleService appointmentScheduleService)
        {
            _appointmentScheduleService = appointmentScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorAppointments()
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            var allDoctorAppointments = await _appointmentScheduleService.AllDoctorAppointments(currentUserID);

            if (!allDoctorAppointments.Any()) return NotFound();
            return Ok(allDoctorAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorAppointmentsByPatientId([FromQuery] string patientId)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            var allDoctorAppointments = await _appointmentScheduleService.GetAllDoctorAppointmentsByPatientId(patientId,currentUserID);

            if (!allDoctorAppointments.Any()) return NotFound();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorAppointmentsByDate([FromQuery] string date)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            var allDoctorAppointments = await _appointmentScheduleService.GetAllDoctorAppointmentsByDate(DateTime.Parse(date), currentUserID);

            if (!allDoctorAppointments.Any()) return NotFound();
            return Ok();
        }


    }
}
