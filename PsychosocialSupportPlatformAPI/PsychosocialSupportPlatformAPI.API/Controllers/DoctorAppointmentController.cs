using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Appointments;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
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
        private readonly IAppointmentService _appointmentService;


        public DoctorAppointmentController(
            IAppointmentScheduleService appointmentScheduleService,
            IUserService userService,
            IAppointmentService appointmentService)
        {
            _appointmentScheduleService = appointmentScheduleService;
            _userService = userService;
            _appointmentService = appointmentService;
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
        public async Task<IActionResult> GetAllPastDoctorAppointmentsByPatientSlug([FromQuery] string patientSlug)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            var allDoctorAppointments = await _appointmentScheduleService.GetAllPastDoctorAppointmentsByPatientSlug(patientSlug, currentUserID);

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


        [HttpPatch]
        public async Task<IActionResult> CancelDoctorAppointment([FromBody] CancelDoctorAppointmentDTO cancelDoctorAppointmentDTO)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            if (cancelDoctorAppointmentDTO == null) return BadRequest();

            await _appointmentService.CancelDoctorAppointment(cancelDoctorAppointmentDTO, currentUserID);

            return Ok();

        }
    }
}
