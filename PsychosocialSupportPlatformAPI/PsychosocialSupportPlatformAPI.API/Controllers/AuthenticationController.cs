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
        public async Task<IActionResult> RegisterForPatient([FromBody] RegisterPatientDto model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                RegisterResponse result = await _authService.RegisterForPatient(model, cancellationToken);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                LoginResponse result = await _authService.LoginUserAsync(model, cancellationToken);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            await _authService.LogOutUser(currentUserId, cancellationToken);
            return Ok();
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
        public async Task<IActionResult> ResetPassword([FromQuery, Required] string token, [FromBody] ResetPasswordDto model, CancellationToken cancellationToken)
        {
            await _authService.ResetPassword(token, model, cancellationToken);
            return Ok("Şifre Değiştirildi");
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromQuery, Required] string email, CancellationToken cancellationToken)
        {
            await _authService.ForgotPassword(email, cancellationToken);
            return Ok("Şifre Değiştirme Bağlantısı Mail Adresinize Gönderildi");
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery, Required] string email, [FromQuery, Required] string token, CancellationToken cancellationToken)
        {
            await _authService.ConfirmEmail(email, token, cancellationToken);
            return Ok("Hesabınız Doğrulandı");
        }


        [HttpPost]
        public async Task<IActionResult> LoginViaGoogle([FromBody] string token, CancellationToken cancellationToken)
        {
            if (token != null)
            {
                LoginResponse result = await _authService.LoginUserViaGoogle(token, cancellationToken);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> LoginViaFacebook([FromBody] string token, CancellationToken cancellationToken)
        {
            if (token != null)
            {
                LoginResponse result = await _authService.LoginUserViaFacebook(token, cancellationToken);

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
