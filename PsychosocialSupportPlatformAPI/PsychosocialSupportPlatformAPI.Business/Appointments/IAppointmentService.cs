﻿using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.Business.Appointments
{
    public interface IAppointmentService
    {
        Task<object> GetPatientAppointmentsByPatientId(string patientId);
        Task<GetPatientAppointmentDTO?> GetPatientAppointmentById(int patientAppointmentId, string patientId);

        Task CancelPatientAppointment(CancelPatientAppointmentDTO cancelPatientAppointmentDTO, string patientId);
        Task<bool> MakeAppointment(string patientId, MakeAppointmentDTO makeAppointmentDTO);

    }
}
