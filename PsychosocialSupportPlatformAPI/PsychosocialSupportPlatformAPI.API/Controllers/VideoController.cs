using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Videos;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideos()
        {
            var videos = await _videoService.GetAllVideos();
            if (videos == null || videos.Count == 0)
            {
                return NotFound("Video Bulunamdı");
            }
            return Ok(videos);
        }

        [HttpGet]
        public async Task<IActionResult> GetVideoById([FromQuery] int videoID)
        {
            var video = await _videoService.GetVideoById(videoID);
            if (video == null)
            {
                return NotFound("Video Bulunamdı");
            }
            return Ok(video);
        }
    }
}
