using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.ResponseModel;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterForDoctor(RegisterDoctorDto model, CancellationToken cancellationToken);
        Task<RegisterResponse> RegisterForPatient(RegisterPatientDto model, CancellationToken cancellationToken);
        Task<LoginResponse> LoginUserAsync(LoginDto model, CancellationToken cancellationToken);
        Task<JwtTokenDTO?> LoginWithRefreshToken(string refreshToken);
        Task ResetPassword(string token, ResetPasswordDto model, CancellationToken cancellationToken);
        Task ForgotPassword(string email, CancellationToken cancellationToken);
        Task ConfirmEmail(string email, string token, CancellationToken cancellationToken);
        Task<LoginResponse> LoginUserViaGoogle(string token, CancellationToken cancellationToken);
        Task<LoginResponse> LoginUserViaFacebook(string token, CancellationToken cancellationToken);
        Task LogOutUser(string currentUserId, CancellationToken cancellationToken);
    }
}
