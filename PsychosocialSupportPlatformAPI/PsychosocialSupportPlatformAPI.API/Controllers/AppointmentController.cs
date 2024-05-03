using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Appointments;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Patient")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetPatientAppointments()
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            var patientAppointments = await _appointmentService.GetPatientAppointmentsByPatientId(currentUserID);

            if (patientAppointments == null) return NotFound();

            return Ok(patientAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientAppointmentById([FromQuery] int patientAppointmentID)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            GetPatientAppointmentDTO? patientAppointments = await _appointmentService.GetPatientAppointmentById(patientAppointmentID, currentUserID);

            if (patientAppointments == null) return NotFound();

            return Ok(patientAppointments);
        }

        [HttpPatch]
        public async Task<IActionResult> MakeAppointment([FromBody] MakeAppointmentDTO makeAppointmentDTO)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            if (await _appointmentService.MakeAppointment(currentUserID, makeAppointmentDTO))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> CancelPatientAppointment([FromBody] CancelPatientAppointmentDTO cancelPatientAppointmentDTO)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            if (cancelPatientAppointmentDTO == null) return BadRequest();

            await _appointmentService.CancelPatientAppointment(cancelPatientAppointmentDTO, currentUserID);

            return Ok();

        }
    }
}
