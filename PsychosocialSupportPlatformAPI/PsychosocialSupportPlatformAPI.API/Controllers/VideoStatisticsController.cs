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
        public async Task<IActionResult> AddVideoStatistics([FromBody] AddVideoStatisticsDTO addVideoStatisticsDTO)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            await _videoStatisticsService.AddVideoStatistics(currentUserID, addVideoStatisticsDTO);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteVideoStatistics([FromBody] int statisticsID)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            await _videoStatisticsService.DeleteVideoStatistics(statisticsID);
            return Ok();
        }


        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetCurrentUserAllVideoStatistics()
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            var allVideoStatistics = await _videoStatisticsService.GetAllVideoStatisticsByPatientId(currentUserID);
            if (!allVideoStatistics.Any()) return NotFound();
            return Ok(allVideoStatistics);
        }


        [HttpGet]
        public async Task<IActionResult> GetVideoStatisticsByID([FromQuery] int statisticsID)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            var videoStatisticsByID = await _videoStatisticsService.GetVideoStatisticsByID(statisticsID);
            if (videoStatisticsByID == null) return NotFound();
            return Ok(videoStatisticsByID);
        }


        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetAllVideoStatisticsByPatientUserName([FromQuery] string patientUserName)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            var allVideoStatistics = await _videoStatisticsService.GetAllVideoStatisticsByPatientUserName(patientUserName, currentUserID);
            if (!allVideoStatistics.Any()) return NotFound();

            return Ok(allVideoStatistics);
        }
    }
}
