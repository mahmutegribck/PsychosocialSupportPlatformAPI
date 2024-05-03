using Newtonsoft.Json;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.FacebookAuthDTOs
{
    public class FacebookUserDataDto
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }


    }
}
