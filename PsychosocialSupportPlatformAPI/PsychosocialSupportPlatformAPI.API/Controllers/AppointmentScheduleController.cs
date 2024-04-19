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

        [HttpPost]
        public async Task<IActionResult> AddAppointmentSchedule([FromBody] AddAppointmentScheduleDTO addAppointmentScheduleDTO)
        {
            await _appointmentScheduleService.AddAppointmentSchedule(addAppointmentScheduleDTO);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointmentSchedules([FromRoute] DateTime day)
        {
            var allAppointmentSchedules = await _appointmentScheduleService.GetAllAppointmentSchedules(day);
            if (!allAppointmentSchedules.Any()) return NotFound();

            return Ok(allAppointmentSchedules);
        }
    }
}
