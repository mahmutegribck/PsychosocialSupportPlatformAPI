using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Business.Messages;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorDTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorTitle;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.PatientDTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<Patient> _patientManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(
            IMapper mapper,
            UserManager<Patient> patientManager,
            IUserService userService,
            IMessageService messageService,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _mapper = mapper;
            _patientManager = patientManager;
            _userService = userService;
            _messageService = messageService;
            _webHostEnvironment = webHostEnvironment;
        }



        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDoctorTitles()
        {
            IEnumerable<GetDoctorTitleDTO> doctorTitles = await _userService.GetAllDoctorTitles();
            if (!doctorTitles.Any()) return NotFound();
            return Ok(doctorTitles);
        }


        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = _mapper.Map<List<GetPatientDto>>(await _patientManager.Users.ToListAsync());
            if (patients.Count != 0)
            {
                return Ok(patients);
            }
            return NotFound("Kullanici Bulunamadi");
        }


        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUserID = User.Identity?.Name;

            if (currentUserID != null)
            {
                var user = await _userService.GetUserByID(currentUserID);

                if (user != null)
                {
                    return Ok(user);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById([FromQuery] string userID)
        {
            if (userID != null)
            {
                var user = await _userService.GetUserByID(userID);

                if (user != null)
                {
                    return Ok(user);
                }
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> GetUserBySlug([FromQuery] string userSlug)
        {
            if (userSlug != null)
            {
                var user = await _userService.GetUserBySlug(userSlug);

                if (user != null)
                {
                    return Ok(user);
                }
            }
            return NotFound();
        }


        [HttpDelete]
        [Authorize(Roles = "Doctor,Patient")]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            var currentUserID = User.Identity?.Name;

            if (currentUserID != null)
            {
                var result = await _userService.DeleteUser(currentUserID);
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
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDTO updateDoctorDTO)
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            IdentityResult result = await _userService.UpdateDoctor(currentUserID, updateDoctorDTO);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPut]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientDTO updatePatientDTO)
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            IdentityResult result = await _userService.UpdatePatient(currentUserID, updatePatientDTO);

            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPatch]
        public async Task<IActionResult> UploadProfileImage([FromForm] UserProfileImageUploadDTO userProfileImageUploadDTO)
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            await _userService.UploadProfileImage(userProfileImageUploadDTO.File, currentUserID, _webHostEnvironment.WebRootPath);
            return Ok();
        }


        [HttpPatch]
        public async Task<IActionResult> DeleteProfileImage()
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            await _userService.DeleteProfileImage(currentUserID);
            return Ok();
        }


        [HttpPatch]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            await _userService.ChangePassword(changePasswordDTO, currentUserID);
            return Ok();
        }



        [HttpPost]
        public async Task<IActionResult> AddMessageDeneme(string SenderId, string ReceiverId, string Text)
        {
            SendMessageDto sendMessageDto = new SendMessageDto();

            sendMessageDto.SendedTime = DateTime.Now;
            sendMessageDto.ReceiverId = ReceiverId;
            sendMessageDto.Text = Text;
            sendMessageDto.SenderId = SenderId;
            await _messageService.AddMessage(sendMessageDto);
            return Ok();
        }
    }
}
