using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;

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
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> CreateDoctorSchedule([FromBody] CreateDoctorScheduleDTO[] createDoctorScheduleDTOs)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();
            await _doctorScheduleService.CreateDoctorSchedule(createDoctorScheduleDTOs, currentUserID);
            return Ok();
        }


        [HttpDelete]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteDoctorSchedule([FromQuery] int doctorScheduleId)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();
            await _doctorScheduleService.DeleteDoctorSchedule(currentUserID, doctorScheduleId);
            return Ok();
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDoctorSchedule()
        {
            var allDoctorSchedule = await _doctorScheduleService.GetAllDoctorSchedule();
            if (allDoctorSchedule == null) return NotFound();
            return Ok(allDoctorSchedule);
        }


        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDoctorSchedule()
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();
            IEnumerable<GetDoctorScheduleDTO> allDoctorSchedule = await _doctorScheduleService.GetAllDoctorScheduleById(currentUserID);
            if (allDoctorSchedule == null) return NotFound();
            return Ok(allDoctorSchedule);
        }


        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetDoctorScheduleById([FromQuery] int scheduleId)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();
            GetDoctorScheduleDTO doctorSchedule = await _doctorScheduleService.GetDoctorScheduleById(currentUserID, scheduleId);
            if (doctorSchedule == null) return NotFound();
            return Ok(doctorSchedule);
        }


        [HttpPut]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> UpdateDoctorSchedule([FromBody] UpdateDoctorScheduleDTO updateDoctorScheduleDTO)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();
            await _doctorScheduleService.UpdateDoctorSchedule(updateDoctorScheduleDTO, currentUserID);
            return Ok();
        }
    }
}
