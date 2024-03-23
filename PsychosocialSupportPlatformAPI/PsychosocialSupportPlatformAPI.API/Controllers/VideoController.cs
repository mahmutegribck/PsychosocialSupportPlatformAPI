using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.IdentityModel.Protocols;
using PsychosocialSupportPlatformAPI.Business.Videos;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;
        private readonly IWebHostEnvironment _webHostEnviroprivatenment;
        public VideoController(IVideoService videoService, IWebHostEnvironment webHostEnviroprivatenment)
        {
            _videoService = videoService;
            _webHostEnviroprivatenment = webHostEnviroprivatenment;

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
        public async Task<IActionResult> DeleteVideo([FromQuery] int videoID)
        {
            await _videoService.DeleteVideo(videoID);
            return Ok("Video Başarıyla Silindi");
        }
    }
}
