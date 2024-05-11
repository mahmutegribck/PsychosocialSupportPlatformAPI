using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs.Doctor;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorAppointmentController : ControllerBase
    {
        private readonly IAppointmentScheduleService _appointmentScheduleService;
        private readonly IUserService _userService;

        public DoctorAppointmentController(IAppointmentScheduleService appointmentScheduleService, IUserService userService)
        {
            _appointmentScheduleService = appointmentScheduleService;
            _userService = userService;
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

            var allDoctorAppointments = await _appointmentScheduleService.GetAllDoctorAppointmentsByPatientId(patientId, currentUserID);

            if (!allDoctorAppointments.Any()) return NotFound();
            return Ok(allDoctorAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorAppointmentsByDate([FromQuery] string date)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            IEnumerable<GetDoctorAppointmentDTO> allDoctorAppointments = await _appointmentScheduleService.GetAllDoctorAppointmentsByDate(DateTime.Parse(date), currentUserID);

            if (!allDoctorAppointments.Any()) return NotFound();
            return Ok(allDoctorAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            IEnumerable<GetPatientDto> allPatients = await _userService.GetAllPatientsByDoctorId(currentUserID);
            if (!allPatients.Any()) return NotFound();

            return Ok(allPatients);
        }
    }
}
