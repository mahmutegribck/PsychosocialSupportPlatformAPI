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
        public async Task<IActionResult> GetAllVideos()
        {
            var videos = await _videoService.GetAllVideos();
            if (videos == null || videos.Count == 0)
            {
                return NotFound("Video Bulunamdı");
            }
            return Ok(await _videoService.GetAllVideos());
        }

        [HttpPost, DisableRequestSizeLimit]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadVideo([FromForm] UploadVideoDTO uploadVideoDTO)
        {
            try
            {
                if (uploadVideoDTO.File == null && uploadVideoDTO.File!.Length == 0)
                {
                    return BadRequest("Video Bulunamadı.");
                }
                await _videoService.UploadVideo(uploadVideoDTO);
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest("Video Uzantısı MP4 Olmalıdır.");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVideo([FromQuery] int videoID)
        {
            await _videoService.DeleteVideo(videoID);
            return Ok("Video Başarıyla Silindi");
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVideo([FromBody] UpdateVideoDTO updateVideoDTO)
        {
            await _videoService.UpdateVideo(updateVideoDTO);
            return Ok();
        }
    }
}
