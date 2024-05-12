using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments;
using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class AppointmentStatisticsController : ControllerBase
    {
        private readonly IAppointmentStatisticsService _appointmentStatisticsService;

        public AppointmentStatisticsController(IAppointmentStatisticsService appointmentStatisticsService)
        {
            _appointmentStatisticsService = appointmentStatisticsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointmentStatistics([FromBody] AddAppointmentStatisticsDTO addAppointmentStatisticsDTO)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            if (addAppointmentStatisticsDTO == null) return BadRequest();

            await _appointmentStatisticsService.AddAppointmentStatistics(addAppointmentStatisticsDTO, currentUserID);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAppointmentStatistics([FromBody] UpdateAppointmentStatisticsDTO updateAppointmentStatisticsDTO)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            if (updateAppointmentStatisticsDTO == null) return BadRequest();

            await _appointmentStatisticsService.UpdateAppointmentStatistics(updateAppointmentStatisticsDTO, currentUserID);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAppointmentStatistics([FromBody] DeleteAppointmentStatisticsDTO deleteAppointmentStatisticsDTO)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            if (deleteAppointmentStatisticsDTO == null) return BadRequest();

            await _appointmentStatisticsService.DeleteAppointmentStatistics(deleteAppointmentStatisticsDTO, currentUserID);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppoinmentStatisticsByPatientUserName([FromQuery] string patientUserName)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            var allAppoinmentStatistics = await _appointmentStatisticsService.GetAllPatientAppointmentStatisticsByPatientUserName(patientUserName, currentUserID);
            if (!allAppoinmentStatistics.Any()) return NotFound();

            return Ok(allAppoinmentStatistics);
        }
    }
}
