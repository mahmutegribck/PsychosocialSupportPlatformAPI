using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments;
using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> AddAppointmentStatistics([FromBody] AddAppointmentStatisticsDTO addAppointmentStatisticsDTO, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            if (addAppointmentStatisticsDTO == null) return BadRequest();

            await _appointmentStatisticsService.AddAppointmentStatistics(addAppointmentStatisticsDTO, currentUserId, cancellationToken);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAppointmentStatistics([FromBody] UpdateAppointmentStatisticsDTO updateAppointmentStatisticsDTO, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            if (updateAppointmentStatisticsDTO == null) return BadRequest();

            await _appointmentStatisticsService.UpdateAppointmentStatistics(updateAppointmentStatisticsDTO, currentUserId, cancellationToken);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAppointmentStatistics([FromBody] DeleteAppointmentStatisticsDTO deleteAppointmentStatisticsDTO, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            if (deleteAppointmentStatisticsDTO == null) return BadRequest();

            await _appointmentStatisticsService.DeleteAppointmentStatistics(deleteAppointmentStatisticsDTO, currentUserId, cancellationToken);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAppoinmentStatisticsByPatientUserName([FromQuery, Required] string patientUserName, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            var allAppoinmentStatistics = await _appointmentStatisticsService.GetAllPatientAppointmentStatisticsByPatientUserName(patientUserName, currentUserId, cancellationToken);
            if (!allAppoinmentStatistics.Any()) return NotFound();

            return Ok(allAppoinmentStatistics);
        }
    }
}
