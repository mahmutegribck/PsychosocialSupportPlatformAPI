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
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            var allDoctorAppointments = await _appointmentScheduleService.AllDoctorAppointments(currentUserId);

            if (!allDoctorAppointments.Any()) return NotFound();
            return Ok(allDoctorAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorAppointmentsByPatientId([FromQuery] string patientId)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            var allDoctorAppointments = await _appointmentScheduleService.GetAllDoctorAppointmentsByPatientId(patientId, currentUserId);

            if (!allDoctorAppointments.Any()) return NotFound();
            return Ok(allDoctorAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPastDoctorAppointmentsByPatientSlug([FromQuery] string patientSlug)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            var allDoctorAppointments = await _appointmentScheduleService.GetAllPastDoctorAppointmentsByPatientSlug(patientSlug, currentUserId);

            if (!allDoctorAppointments.Any()) return NotFound();
            return Ok(allDoctorAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctorAppointmentsByDate([FromQuery] string date)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            IEnumerable<GetDoctorAppointmentDTO> allDoctorAppointments = await _appointmentScheduleService.GetAllDoctorAppointmentsByDate(DateTime.Parse(date), currentUserId);

            if (!allDoctorAppointments.Any()) return NotFound();
            return Ok(allDoctorAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            IEnumerable<GetPatientDto> allPatients = await _userService.GetAllPatientsByDoctorId(currentUserId);
            if (!allPatients.Any()) return NotFound();

            return Ok(allPatients);
        }

        [HttpPatch]
        public async Task<IActionResult> CreateAppointmentForPatient([FromBody] CreateAppointmentForPatientDTO createAppointmentForPatientDTO)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            if (createAppointmentForPatientDTO == null) return BadRequest();

            await _appointmentService.CreateAppointmentForPatient(currentUserId, createAppointmentForPatientDTO);
            return Ok();


        }

        [HttpPatch]
        public async Task<IActionResult> CancelDoctorAppointment([FromBody] CancelDoctorAppointmentDTO cancelDoctorAppointmentDTO)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            if (cancelDoctorAppointmentDTO == null) return BadRequest();

            await _appointmentService.CancelDoctorAppointment(cancelDoctorAppointmentDTO, currentUserId);

            return Ok();

        }
    }
}
