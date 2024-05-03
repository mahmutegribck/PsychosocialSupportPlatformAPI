using Newtonsoft.Json;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.FacebookAuthDTOs
{
    public class FacebookUserDto
    {
        [JsonProperty("data")]
        public FacebookData Data { get; set; }

    }
    public class FacebookData
    {
        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}