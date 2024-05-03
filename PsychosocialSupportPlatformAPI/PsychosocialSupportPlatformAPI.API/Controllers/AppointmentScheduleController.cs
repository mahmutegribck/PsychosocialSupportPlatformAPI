using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentScheduleController : ControllerBase
    {
        private readonly IAppointmentScheduleService _appointmentScheduleService;

        public AppointmentScheduleController(IAppointmentScheduleService appointmentScheduleService)
        {
            _appointmentScheduleService = appointmentScheduleService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAppointmentSchedules([FromQuery] string date)
        {
            var allAppointmentSchedules = await _appointmentScheduleService.GetAllAppointmentSchedules(DateTime.Parse(date));
            if (!allAppointmentSchedules.Any()) return NotFound();

            return Ok(allAppointmentSchedules);
        }

        [HttpPatch]
        public async Task<IActionResult> MakeAppointment([FromBody] MakeAppointmentDTO makeAppointmentDTO)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            if (await _appointmentScheduleService.MakeAppointment(currentUserID, makeAppointmentDTO))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
