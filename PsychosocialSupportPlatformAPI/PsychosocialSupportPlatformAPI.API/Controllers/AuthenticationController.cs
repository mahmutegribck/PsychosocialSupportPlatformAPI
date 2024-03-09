using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Business.Auth.AuthService.ResponseModel;
using PsychosocialSupportPlatformAPI.Business.Auth.JwtToken.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        

        [HttpPost]
        public async Task<IActionResult> RegisterForDoctor([FromBody] RegisterDoctorDto model)
        {
            if (ModelState.IsValid)
            {
                RegisterResponse result = await _authService.RegisterForDoctor(model);
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
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                LoginResponse result = await _authService.ResetPasswordAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest();
        }
    }
}
