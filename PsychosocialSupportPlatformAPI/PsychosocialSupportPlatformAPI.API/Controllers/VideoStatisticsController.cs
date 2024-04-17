using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Statistics;
using PsychosocialSupportPlatformAPI.Business.Statistics.DTOs;

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
        public async Task<IActionResult> CreateVideoStatistics([FromBody] CreateVideoStatisticsDTO createVideoStatisticsDTO)
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return NotFound();
            await _videoStatisticsService.CreateVideoStatistics(currentUserID, createVideoStatisticsDTO);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVideoStatistics([FromBody] UpdateVideoStatisticsDTO updateVideoStatisticsDTO)
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return NotFound();
            await _videoStatisticsService.UpdateVideoStatistics(currentUserID, updateVideoStatisticsDTO);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVideoStatistics([FromBody] int statisticsID)
        {
            await _videoStatisticsService.DeleteVideoStatistics(statisticsID);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> GetAllVideoStatistics()
        {
            var allVideoStatistics = await _videoStatisticsService.GetAllVideoStatistics();
            if (!allVideoStatistics.Any()) return NotFound();
            return Ok(allVideoStatistics);
        }

        [HttpGet]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> GetCurrentUserAllVideoStatistics()
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return NotFound();
            var allVideoStatistics = await _videoStatisticsService.GetAllVideoStatisticsByPatientID(currentUserID);
            if (!allVideoStatistics.Any()) return NotFound();
            return Ok(allVideoStatistics);
        }

        [HttpGet]
        public async Task<IActionResult> GetVideoStatisticsByID([FromQuery] int statisticsID)
        {
            var videoStatisticsByID = await _videoStatisticsService.GetVideoStatisticsByID(statisticsID);
            if (videoStatisticsByID == null) return NotFound();
            return Ok(videoStatisticsByID);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Doctor")]
        public async Task<IActionResult> GetAllVideoStatisticsByPatientID([FromQuery] string patientID)
        {
            var allVideoStatisticsByPatientID = await _videoStatisticsService.GetAllVideoStatisticsByPatientID(patientID);
            if (!allVideoStatisticsByPatientID.Any()) return NotFound();
            return Ok(allVideoStatisticsByPatientID);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetVideoStatisticsByPatientID([FromQuery] string patientID)
        //{
        //    var videoStatisticsByPatientID = await _videoStatisticsService.GetVideoStatisticsByPatientID(patientID);
        //    if (videoStatisticsByPatientID == null) return NotFound();
        //    return Ok(videoStatisticsByPatientID);
        //}
    }
}
