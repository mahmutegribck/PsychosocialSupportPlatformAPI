using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Videos;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;

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
        public async Task<IActionResult> GetAllVideos(CancellationToken cancellationToken)
        {
            var videos = await _videoService.GetAllVideos(cancellationToken);
            if (!videos.Any())
            {
                return NotFound("Video Bulunamdı");
            }
            return Ok(videos);
        }

        [HttpGet]
        public async Task<IActionResult> GetVideoByVideoSlug([FromQuery] string videoSlug, CancellationToken cancellationToken)
        {
            GetVideoDTO? video = await _videoService.GetVideoByVideoSlug(videoSlug, cancellationToken);
            if (video == null)
            {
                return NotFound("Video Bulunamdı");
            }
            return Ok(video);
        }
    }
}
