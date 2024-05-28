using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs.Doctor;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.Business.DoctorSchedules;
using PsychosocialSupportPlatformAPI.Business.MLModel;
using PsychosocialSupportPlatformAPI.Business.MLModel.DTOs;
using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments;
using PsychosocialSupportPlatformAPI.Business.Statistics.Videos;
using PsychosocialSupportPlatformAPI.Business.Users;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs.DoctorTitle;
using PsychosocialSupportPlatformAPI.Business.Videos;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;
using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IDoctorScheduleService _doctorScheduleService;
        private readonly IUserService _userService;
        private readonly IVideoService _videoService;
        private readonly IVideoStatisticsService _videoStatisticsService;
        private readonly IAppointmentStatisticsService _appointmentStatisticsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAppointmentScheduleService _appointmentScheduleService;
        private readonly IMLModelService _modelBuilderService;



        public AdminController(
            IDoctorScheduleService doctorScheduleService,
            IUserService userService,
            IVideoService videoService,
            IVideoStatisticsService videoStatisticsService,
            IAppointmentStatisticsService appointmentStatisticsService,
            IWebHostEnvironment webHostEnvironment,
            IAppointmentScheduleService appointmentScheduleService,
            IMLModelService modelBuilderService
            )
        {
            _doctorScheduleService = doctorScheduleService;
            _userService = userService;
            _videoService = videoService;
            _videoStatisticsService = videoStatisticsService;
            _appointmentStatisticsService = appointmentStatisticsService;
            _webHostEnvironment = webHostEnvironment;
            _appointmentScheduleService = appointmentScheduleService;
            _modelBuilderService = modelBuilderService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAIModel([FromForm] UploadDataSetDTO uploadDataSetDTO, CancellationToken cancellationToken)
        {
            await _modelBuilderService.CreateAIModel(uploadDataSetDTO, cancellationToken);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUnConfirmedDoctor(CancellationToken cancellationToken)
        {
            IEnumerable<GetDoctorDto> unConfirmedDoctors = await _userService.GetAllUnConfirmedDoctor(cancellationToken);
            if (!unConfirmedDoctors.Any()) return NotFound();
            return Ok(unConfirmedDoctors);
        }


        [HttpPatch]
        public async Task<IActionResult> ConfirmDoctor([FromQuery] string doctorUserName, CancellationToken cancellationToken)
        {
            await _userService.ConfirmDoctor(doctorUserName, cancellationToken);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> AddDoctorTitle([FromBody] AddDoctorTitleDTO addDoctorTitleDTO, CancellationToken cancellationToken)
        {
            await _userService.AddDoctorTitle(addDoctorTitleDTO, cancellationToken);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteDoctorTitle([FromQuery] int doctorTitleId, CancellationToken cancellationToken)
        {
            await _userService.DeleteDoctorTitle(doctorTitleId, cancellationToken);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDoctorSchedulesByDate([FromQuery] string day, CancellationToken cancellationToken)
        {
            IEnumerable<object> allDoctorSchedules = await _doctorScheduleService.GetAllDoctorSchedulesByDate(DateTime.Parse(day), cancellationToken);
            if (!allDoctorSchedules.Any()) return NotFound();
            return Ok(allDoctorSchedules);
        }


        [HttpGet]
        public async Task<IActionResult> GetDoctorAppointmentByDateAndTimeRange(GetDoctorAppointmentByDateAndTimeRangeDTO getDoctorAppointmentByDateAndTimeRangeDTO, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            GetDoctorAppointmentDTO? allDoctorAppointments = await _appointmentScheduleService.GetDoctorAppointmentByDateAndTimeRange(getDoctorAppointmentByDateAndTimeRangeDTO, cancellationToken);

            if (allDoctorAppointments == null) return NotFound();

            return Ok(allDoctorAppointments);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] string id, CancellationToken cancellationToken)
        {
            if (id != null)
            {
                IdentityResult result = await _userService.DeleteUser(id, cancellationToken);
                if (result.Succeeded)
                {
                    return Ok();
                }
                return BadRequest(result.Errors);
            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPatients(CancellationToken cancellationToken)
        {
            IEnumerable<GetPatientDto> patients = await _userService.GetAllPatients(cancellationToken);

            if (patients.Any())
            {
                return Ok(patients);
            }
            return NotFound("Kullanici Bulunamadi");
        }


        [HttpGet]
        public async Task<IActionResult> GetAllDoctors(CancellationToken cancellationToken)
        {
            IEnumerable<GetDoctorDto> doctors = await _userService.GetAllDoctors(cancellationToken);
            if (doctors.Any())
            {
                return Ok(doctors);
            }
            return NotFound("Kullanici Bulunamadi");
        }


        [HttpGet]
        public async Task<IActionResult> GetAllVideoStatistics(CancellationToken cancellationToken)
        {
            var allVideoStatistics = await _videoStatisticsService.GetAllVideoStatistics(cancellationToken);
            if (!allVideoStatistics.Any()) return NotFound();
            return Ok(allVideoStatistics);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllVideoStatisticsByPatientUserName([FromQuery, Required] string patientUserName, CancellationToken cancellationToken)
        {
            var allVideoStatisticsByPatientID = await _videoStatisticsService.GetAllVideoStatisticsByPatientUserName(patientUserName, cancellationToken);
            if (!allVideoStatisticsByPatientID.Any()) return NotFound();
            return Ok(allVideoStatisticsByPatientID);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPatientAppointmentStatistics(CancellationToken cancellationToken)
        {
            var allPatientAppointmentStatistics = await _appointmentStatisticsService.GetAllPatientAppointmentStatistics(cancellationToken);
            if (!allPatientAppointmentStatistics.Any()) return NotFound();
            return Ok(allPatientAppointmentStatistics);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPatientAppointmentStatisticsByDoctorUserName([FromQuery, Required] string doctorUserName, CancellationToken cancellationToken)
        {
            var allPatientAppointmentStatistics = await _appointmentStatisticsService.GetAllPatientAppointmentStatisticsByDoctorUserName(doctorUserName, cancellationToken);
            if (!allPatientAppointmentStatistics.Any()) return NotFound();
            return Ok(allPatientAppointmentStatistics);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPatientAppointmentStatisticsByPatientUserName([FromQuery, Required] string patientUserName, CancellationToken cancellationToken)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            var allPatientAppointmentStatistics = await _appointmentStatisticsService.GetAllPatientAppointmentStatisticsByPatientUserName(patientUserName, cancellationToken);
            if (!allPatientAppointmentStatistics.Any()) return NotFound();
            return Ok(allPatientAppointmentStatistics);
        }


        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadVideo([FromForm] UploadVideoDTO uploadVideoDTO, CancellationToken cancellationToken)
        {
            try
            {
                if (uploadVideoDTO.File == null && uploadVideoDTO.File!.Length == 0)
                {
                    return BadRequest("Video Bulunamadı.");
                }
                await _videoService.UploadVideo(uploadVideoDTO, _webHostEnvironment.WebRootPath, cancellationToken);
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest("Video Uzantısı MP4 Olmalıdır.");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteVideo([FromQuery, Required] int videoID, CancellationToken cancellationToken)
        {
            await _videoService.DeleteVideo(videoID, cancellationToken);
            return Ok("Video Başarıyla Silindi");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateVideo([FromBody] UpdateVideoDTO updateVideoDTO, CancellationToken cancellationToken)
        {
            await _videoService.UpdateVideo(updateVideoDTO, cancellationToken);
            return Ok();
        }
    }
}
