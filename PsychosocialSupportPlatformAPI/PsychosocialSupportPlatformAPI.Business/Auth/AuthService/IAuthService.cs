using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.ResponseModel;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Auth.AuthService
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterForDoctor(RegisterDoctorDto model);
        Task<RegisterResponse> RegisterForPatient(RegisterPatientDto model);

        Task<LoginResponse> LoginUserAsync(LoginDto model);

        Task<JwtTokenDTO?> LoginWithRefreshToken(string refreshToken);
        Task<LoginResponse> ResetPasswordAsync(ResetPasswordDto model);

        Task<LoginResponse> LoginUserViaGoogle(string token);
        Task<LoginResponse> LoginUserViaFacebook(string token);
    }
}
