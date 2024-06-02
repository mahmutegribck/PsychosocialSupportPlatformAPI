using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorTitle;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.PatientDTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
            IUserService userService
            )
        {
            _userService = userService;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDoctorTitles(CancellationToken cancellationToken)
        {
            IEnumerable<GetDoctorTitleDTO> doctorTitles = await _userService.GetAllDoctorTitles(cancellationToken);
            if (!doctorTitles.Any()) return NotFound();
            return Ok(doctorTitles);
        }


        [HttpGet]
        public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
        {
            var currentUserId = User.Identity?.Name;

            if (currentUserId != null)
            {
                var user = await _userService.GetUserByID(currentUserId, cancellationToken);

                if (user != null)
                {
                    return Ok(user);
                }
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] string userID, CancellationToken cancellationToken)
        {
            if (userID != null)
            {
                var user = await _userService.GetUserByID(userID, cancellationToken);

                if (user != null)
                {
                    return Ok(user);
                }
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> GetUserBySlug([FromQuery] string userSlug, CancellationToken cancellationToken)
        {
            if (userSlug != null)
            {
                var user = await _userService.GetUserBySlug(userSlug, cancellationToken);

                if (user != null)
                {
                    return Ok(user);
                }
            }
            return NotFound();
        }


        [HttpDelete]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> DeleteCurrentUser(CancellationToken cancellationToken)
        {
            var currentUserId = User.Identity?.Name;

            if (currentUserId != null)
            {
                var result = await _userService.DeleteUser(currentUserId, cancellationToken);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return NotFound();
            }
            return Unauthorized();
        }


        [HttpPut]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDTO updateDoctorDTO, CancellationToken cancellationToken)
        {
            var currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            IdentityResult result = await _userService.UpdateDoctor(currentUserId, updateDoctorDTO, cancellationToken);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPatch]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateDoctorTitle(int doctorTitleId, CancellationToken cancellationToken)
        {
            var currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            IdentityResult result = await _userService.UpdateDoctorTitle(currentUserId, doctorTitleId, cancellationToken);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPut]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientDTO updatePatientDTO, CancellationToken cancellationToken)
        {
            var currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            IdentityResult result = await _userService.UpdatePatient(currentUserId, updatePatientDTO, cancellationToken);

            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPatch]
        public async Task<IActionResult> UploadProfileImage([FromForm] UserProfileImageUploadDTO userProfileImageUploadDTO, CancellationToken cancellationToken)
        {
            var currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            await _userService.UploadProfileImage(userProfileImageUploadDTO.File, currentUserId, cancellationToken);
            return Ok();
        }


        [HttpPatch]
        public async Task<IActionResult> DeleteProfileImage(CancellationToken cancellationToken)
        {
            var currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            await _userService.DeleteProfileImage(currentUserId, cancellationToken);
            return Ok();
        }


        [HttpPatch]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO, CancellationToken cancellationToken)
        {
            var currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            await _userService.ChangePassword(changePasswordDTO, currentUserId, cancellationToken);
            return Ok();
        }
    }
}
