using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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