﻿    using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.Business.Messages;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserManager<Doctor> _userManagerDoctor;
        private readonly UserManager<Patient> _patientManager;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public UserController(UserManager<Doctor> userManagerDoctor, IMapper mapper, UserManager<Patient> patientManager, UserManager<ApplicationUser> userManager, IUserService userService, IMessageService messageService)
        {
            _userManagerDoctor = userManagerDoctor;
            _mapper = mapper;
            _patientManager = patientManager;
            _userManager = userManager;
            _userService = userService;
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            var doctors = _mapper.Map<List<GetDoctorDto>>(await _userManagerDoctor.Users.ToListAsync());
            if (doctors.Count != 0)
            {
                return Ok(doctors);

            }
            return NotFound("Kullanici Bulunamadi");
        }

        [HttpGet]
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
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var user = await _userService.GetUserByID(currentUser.Id);

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

        [HttpDelete]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                var result = await _userService.DeleteUser(currentUser.Id);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return NotFound();
            }
            return Unauthorized();

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] string id)
        {
            if (id != null)
            {
                var result = await _userService.DeleteUser(id);
                if (result.Succeeded)
                {
                    return Ok();
                }
            }
            return NotFound();
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
