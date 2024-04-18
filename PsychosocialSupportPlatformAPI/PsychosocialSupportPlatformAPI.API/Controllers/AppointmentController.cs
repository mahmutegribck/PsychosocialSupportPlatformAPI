using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Appointments;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules;
using PsychosocialSupportPlatformAPI.Entity.Entities;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IAppointmentScheduleRepository _appointmentScheduleRepository;

        public AppointmentController(IAppointmentService appointmentService, IAppointmentScheduleRepository appointmentScheduleRepository)
        {
            _appointmentService = appointmentService;
            _appointmentScheduleRepository = appointmentScheduleRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreatePatientAppointment([FromBody] CreateAppointmentDTO createAppointmentDTO)
        {
            await _appointmentService.CreatePatientAppointment(createAppointmentDTO);
            return Ok();
        }



        [HttpPost]
        public async Task<IActionResult> DenemeCreatePatientAppointment([FromBody] AppointmentSchedule appointmentSchedule)
        {
            await _appointmentScheduleRepository.AddAppointmentSchedule(appointmentSchedule);
            return Ok();
        }
    }
}
