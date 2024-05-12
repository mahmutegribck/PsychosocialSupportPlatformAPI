using Microsoft.AspNetCore.Http;

namespace PsychosocialSupportPlatformAPI.Business.Users.DTOs
{
    public class UserProfileImageUploadDTO
    {
        public required IFormFile File { get; set; }
    }
}
