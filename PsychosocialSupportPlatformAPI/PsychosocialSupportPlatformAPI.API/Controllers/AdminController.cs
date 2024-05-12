using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments;
using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.Statistics.Videos;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Business.Videos;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IDoctorScheduleService _doctorScheduleService;
        private readonly IUserService _userService;
        private readonly UserManager<Patient> _patientManager;
        private readonly IMapper _mapper;
        private readonly IVideoService _videoService;
        private readonly IVideoStatisticsService _videoStatisticsService;
        private readonly IAppointmentStatisticsService _appointmentStatisticsService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public AdminController(
            IDoctorScheduleService doctorScheduleService,
            IUserService userService,
            UserManager<Patient> patientManager,
            IMapper mapper,
            IVideoService videoService,
            IVideoStatisticsService videoStatisticsService,
            IAppointmentStatisticsService appointmentStatisticsService,
            IWebHostEnvironment webHostEnvironment
            )
        {
            _doctorScheduleService = doctorScheduleService;
            _userService = userService;
            _patientManager = patientManager;
            _mapper = mapper;
            _videoService = videoService;
            _videoStatisticsService = videoStatisticsService;
            _appointmentStatisticsService = appointmentStatisticsService;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDoctorSchedule()
        {
            IEnumerable<object> allDoctorSchedules = await _doctorScheduleService.GetAllDoctorSchedules();
            if (!allDoctorSchedules.Any()) return NotFound();
            return Ok(allDoctorSchedules);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] string id)
        {
            if (id != null)
            {
                IdentityResult result = await _userService.DeleteUser(id);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            return NotFound();
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

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadVideo([FromForm] UploadVideoDTO uploadVideoDTO)
        {
            try
            {
                if (uploadVideoDTO.File == null && uploadVideoDTO.File!.Length == 0)
                {
                    return BadRequest("Video Bulunamadı.");
                }
                await _videoService.UploadVideo(uploadVideoDTO, _webHostEnvironment.WebRootPath);
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest("Video Uzantısı MP4 Olmalıdır.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVideo([FromQuery] int videoID)
        {
            await _videoService.DeleteVideo(videoID);
            return Ok("Video Başarıyla Silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVideo([FromBody] UpdateVideoDTO updateVideoDTO)
        {
            await _videoService.UpdateVideo(updateVideoDTO);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideoStatistics()
        {
            var allVideoStatistics = await _videoStatisticsService.GetAllVideoStatistics();
            if (!allVideoStatistics.Any()) return NotFound();
            return Ok(allVideoStatistics);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVideoStatisticsByPatientID([FromQuery] string patientID)
        {
            var allVideoStatisticsByPatientID = await _videoStatisticsService.GetAllVideoStatisticsByPatientID(patientID);
            if (!allVideoStatisticsByPatientID.Any()) return NotFound();
            return Ok(allVideoStatisticsByPatientID);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatientAppointmentStatistics()
        {
            var allPatientAppointmentStatistics = await _appointmentStatisticsService.GetAllPatientAppointmentStatistics();
            if (!allPatientAppointmentStatistics.Any()) return NotFound();
            return Ok(allPatientAppointmentStatistics);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatientAppointmentStatisticsByDoctorUserName([FromQuery] string doctorUserName)
        {
            var allPatientAppointmentStatistics = await _appointmentStatisticsService.GetAllPatientAppointmentStatisticsByDoctorUserName(doctorUserName);
            if (!allPatientAppointmentStatistics.Any()) return NotFound();
            return Ok(allPatientAppointmentStatistics);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatientAppointmentStatisticsByPatientUserName([FromQuery] string patientUserName)
        {
            string? currentUserID = User.Identity?.Name;
            if (currentUserID == null) return Unauthorized();

            var allPatientAppointmentStatistics = await _appointmentStatisticsService.GetAllPatientAppointmentStatisticsByPatientUserName(patientUserName);
            if (!allPatientAppointmentStatistics.Any()) return NotFound();
            return Ok(allPatientAppointmentStatistics);
        }
    }
}
