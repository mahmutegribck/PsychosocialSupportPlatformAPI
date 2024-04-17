using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleService _doctorScheduleService;

        public DoctorScheduleController(IDoctorScheduleService doctorScheduleService)
        {
            _doctorScheduleService = doctorScheduleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctorSchedule([FromBody] CreateDoctorScheduleDTO createDoctorScheduleDTO)
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();
            await _doctorScheduleService.CreateDoctorSchedule(createDoctorScheduleDTO, currentUserID);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDoctorSchedule([FromQuery] int doctorScheduleId)
        {
            await _doctorScheduleService.DeleteDoctorSchedule(doctorScheduleId);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDoctorSchedule()
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();
            var allDoctorSchedule = await _doctorScheduleService.GetAllDoctorScheduleById(currentUserID);
            if (allDoctorSchedule == null) return NotFound();
            return Ok(allDoctorSchedule);
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorScheduleById([FromQuery] int scheduleId)
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();
            var doctorSchedule = await _doctorScheduleService.GetDoctorScheduleById(currentUserID, scheduleId);
            if (doctorSchedule == null) return NotFound();
            return Ok(doctorSchedule);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateDoctorSchedule([FromBody] UpdateDoctorScheduleDTO updateDoctorScheduleDTO)
        {
            var currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();
            await _doctorScheduleService.UpdateDoctorSchedule(updateDoctorScheduleDTO, currentUserID);
            return Ok();
        }
    }
}
