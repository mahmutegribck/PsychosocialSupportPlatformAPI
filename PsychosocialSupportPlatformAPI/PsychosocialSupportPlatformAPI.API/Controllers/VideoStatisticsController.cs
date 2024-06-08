using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Statistics.Videos;
using PsychosocialSupportPlatformAPI.Business.Statistics.Videos.DTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class VideoStatisticsController : ControllerBase
    {
        private readonly IVideoStatisticsService _videoStatisticsService;
        public VideoStatisticsController(IVideoStatisticsService videoStatisticsService)
        {
            _videoStatisticsService = videoStatisticsService;
        }


        [HttpPost]
        public async Task<IActionResult> AddVideoStatistics([FromBody] AddVideoStatisticsDTO addVideoStatisticsDTO, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            await _videoStatisticsService.AddVideoStatistics(currentUserId, addVideoStatisticsDTO, cancellationToken);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteVideoStatistics([FromBody] int statisticsId, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            await _videoStatisticsService.DeleteVideoStatistics(statisticsId, cancellationToken);
            return Ok();
        }


        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetCurrentUserAllVideoStatistics(CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            var allVideoStatistics = await _videoStatisticsService.GetAllVideoStatisticsByPatientId(currentUserId, cancellationToken);
            if (!allVideoStatistics.Any()) return NotFound();
            return Ok(allVideoStatistics);
        }


        [HttpGet]
        public async Task<IActionResult> GetVideoStatisticsByID([FromQuery] int statisticsId, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            var videoStatisticsByID = await _videoStatisticsService.GetVideoStatisticsById(statisticsId, cancellationToken);
            if (videoStatisticsByID == null) return NotFound();
            return Ok(videoStatisticsByID);
        }


        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetAllVideoStatisticsByPatientUserName([FromQuery] string patientUserName, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            var allVideoStatistics = await _videoStatisticsService.GetAllVideoStatisticsByPatientUserName(patientUserName, currentUserId, cancellationToken);
            if (!allVideoStatistics.Any()) return NotFound();

            return Ok(allVideoStatistics);
        }
    }
}
