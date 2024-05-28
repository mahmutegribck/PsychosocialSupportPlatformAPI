using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class DoctorScheduleController : ControllerBase
    {
        private readonly IDoctorScheduleService _doctorScheduleService;

        public DoctorScheduleController(IDoctorScheduleService doctorScheduleService)
        {
            _doctorScheduleService = doctorScheduleService;
        }


        [HttpPost]
        public async Task<IActionResult> AddDoctorSchedule([FromBody] List<CreateDoctorScheduleDTO> createDoctorScheduleDTOs)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();
            await _doctorScheduleService.AddDoctorSchedule(createDoctorScheduleDTOs, currentUserId);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDoctorSchedule([FromQuery] int doctorScheduleId)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();
            await _doctorScheduleService.DeleteDoctorSchedule(currentUserId, doctorScheduleId);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetDoctorSchedules()
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();
            IEnumerable<GetDoctorScheduleDTO?> allDoctorSchedule = await _doctorScheduleService.GetAllDoctorScheduleById(currentUserId);
            if (allDoctorSchedule == null) return NotFound();
            return Ok(allDoctorSchedule);
        }


        [HttpGet]
        public async Task<IActionResult> GetDoctorScheduleById([FromQuery] int scheduleId)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();
            GetDoctorScheduleDTO? doctorSchedule = await _doctorScheduleService.GetDoctorScheduleById(currentUserId, scheduleId);
            if (doctorSchedule == null) return NotFound();
            return Ok(doctorSchedule);
        }
    }
}
