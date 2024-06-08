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
        public async Task<IActionResult> AddDoctorSchedule([FromBody] List<CreateDoctorScheduleDTO> createDoctorScheduleDTOs, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();
            await _doctorScheduleService.AddDoctorSchedule(createDoctorScheduleDTOs, currentUserId, cancellationToken);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDoctorSchedule([FromQuery] int doctorScheduleId, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();
            await _doctorScheduleService.DeleteDoctorSchedule(currentUserId, doctorScheduleId, cancellationToken);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetDoctorSchedules(CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();
            IEnumerable<GetDoctorScheduleDTO?> allDoctorSchedule = await _doctorScheduleService.GetAllDoctorScheduleById(currentUserId, cancellationToken);
            if (allDoctorSchedule == null) return NotFound();
            return Ok(allDoctorSchedule);
        }


        [HttpGet]
        public async Task<IActionResult> GetDoctorScheduleById([FromQuery] int scheduleId, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();
            GetDoctorScheduleDTO? doctorSchedule = await _doctorScheduleService.GetDoctorScheduleById(currentUserId, scheduleId, cancellationToken);
            if (doctorSchedule == null) return NotFound();
            return Ok(doctorSchedule);
        }
    }
}
