﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Appointments;
using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Patient")]
    public class PatientAppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public PatientAppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetPatientDoctors()
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            IEnumerable<GetPatientDoctorDto> patientDoctors = await _appointmentService.GetPatientDoctorsByPatientId(currentUserId);

            if (!patientDoctors.Any()) return NotFound();

            return Ok(patientDoctors);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientAppointments()
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            var patientAppointments = await _appointmentService.GetPatientAppointmentsByPatientId(currentUserId);

            if (patientAppointments == null) return NotFound();

            return Ok(patientAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatientAppointmentById([FromQuery] int patientAppointmentID)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            GetPatientAppointmentDTO? patientAppointments = await _appointmentService.GetPatientAppointmentById(patientAppointmentID, currentUserId);

            if (patientAppointments == null) return NotFound();

            return Ok(patientAppointments);
        }

        [HttpPatch]
        public async Task<IActionResult> MakeAppointment([FromBody] MakeAppointmentDTO makeAppointmentDTO)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            if (await _appointmentService.MakeAppointment(currentUserId, makeAppointmentDTO))
            {
                return Ok("Randevu Oluşturuldu.");
            }
            return BadRequest("Randevu Oluşturulamadı");
        }

        [HttpPatch]
        public async Task<IActionResult> CancelPatientAppointment([FromBody] CancelPatientAppointmentDTO cancelPatientAppointmentDTO)
        {
            string? currentUserId = User.Identity?.Name;
            if (currentUserId == null) return Unauthorized();

            if (cancelPatientAppointmentDTO == null) return BadRequest();

            await _appointmentService.CancelPatientAppointment(cancelPatientAppointmentDTO, currentUserId);

            return Ok();

        }
    }
}
