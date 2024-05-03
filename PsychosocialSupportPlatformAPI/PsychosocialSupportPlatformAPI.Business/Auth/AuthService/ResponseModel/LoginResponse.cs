using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService.ResponseModel
{
    public class LoginResponse
    {
        public JwtTokenDTO JwtTokenDTO { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

    }
}
