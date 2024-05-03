using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.Business.Auth.JwtToken
{
    public interface IJwtService
    {
        Task<JwtTokenDTO> CreateJwtToken(ApplicationUser user);
        Task<string> GenerateRefreshToken(ApplicationUser user, DateTime accessTokenTime);
        Task<JwtTokenDTO?> GenerateJwtTokenWithRefreshToken(string refreshToken);
    }
}
