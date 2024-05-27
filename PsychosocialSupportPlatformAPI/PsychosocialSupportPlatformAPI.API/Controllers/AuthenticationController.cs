using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.ResponseModel;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterForDoctor([FromBody] RegisterDoctorDto model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                RegisterResponse result = await _authService.RegisterForDoctor(model, cancellationToken);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> RegisterForPatient([FromBody] RegisterPatientDto model)
        {
            if (ModelState.IsValid)
            {
                RegisterResponse result = await _authService.RegisterForPatient(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                LoginResponse result = await _authService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> LoginWithRefreshToken([FromBody] string refreshToken)
        {
            JwtTokenDTO? tokens = await _authService.LoginWithRefreshToken(refreshToken);
            if (tokens != null)
            {
                return Ok(tokens);
            }
            return Unauthorized();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromBody] ResetPasswordDto model)
        {
            await _authService.ResetPassword(token, model);
            return Ok("Şifre Değiştirildi");
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required] string email)
        {
            await _authService.ForgotPassword(email);
            return Ok("Şifre Değiştirme Bağlantısı Mail Adresinize Gönderildi");
        }


        [HttpPost]
        public async Task<IActionResult> LoginViaGoogle([FromBody] string token)
        {
            if (token != null)
            {
                LoginResponse result = await _authService.LoginUserViaGoogle(token);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> LoginViaFacebook([FromBody] string token)
        {
            if (token != null)
            {
                LoginResponse result = await _authService.LoginUserViaFacebook(token);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }
    }
}
