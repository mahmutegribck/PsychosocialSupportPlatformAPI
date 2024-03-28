using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Statistics;
using PsychosocialSupportPlatformAPI.Business.Statistics.DTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
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
            await _videoStatisticsService.CreateVideoStatistics(createVideoStatisticsDTO);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVideoStatistics([FromBody] UpdateVideoStatisticsDTO updateVideoStatisticsDTO)
        {
            await _videoStatisticsService.UpdateVideoStatistics(updateVideoStatisticsDTO);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVideoStatistics([FromBody] int statisticsID)
        {
            await _videoStatisticsService.DeleteVideoStatistics(statisticsID);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideoStatistics()
        {
            var allVideoStatistics = await _videoStatisticsService.GetAllVideoStatistics();
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
        public async Task<IActionResult> GetAllVideoStatisticsByPatientID([FromQuery] string patientID)
        {
            var allVideoStatisticsByPatientID = await _videoStatisticsService.GetAllVideoStatisticsByPatientID(patientID);
            if (!allVideoStatisticsByPatientID.Any()) return NotFound();
            return Ok(allVideoStatisticsByPatientID);
        }

        [HttpGet]
        public async Task<IActionResult> GetVideoStatisticsByPatientID([FromQuery] string patientID)
        {
            var videoStatisticsByPatientID = await _videoStatisticsService.GetVideoStatisticsByPatientID(patientID);
            if (videoStatisticsByPatientID == null) return NotFound();
            return Ok(videoStatisticsByPatientID);
        }
    }
}
