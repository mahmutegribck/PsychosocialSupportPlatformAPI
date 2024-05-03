namespace PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs
{
    public class JwtTokenDTO
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenTime { get; set; }
        public string? RefreshToken { get; set; }

    }
}
