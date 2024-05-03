using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Patient")]

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
    }
}
