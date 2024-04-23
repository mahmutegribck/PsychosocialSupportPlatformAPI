﻿using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.DataAccess.Appointments
{
    public interface IAppointmentRepository
    {
        Task CreatePatientAppointment(Appointment appointment);
        Task DeletePatientAppointment(Appointment appointment);
        Task<Appointment> GetPatientAppointmentById(int appointmentID, string patientID);
        Task<Appointment> GetDoctorAppointmentById(int appointmentID, string doctorID);
        Task<IEnumerable<Appointment>> GetAllPatientAppointments(string patientID);
        Task<IEnumerable<Appointment>> GetAllPatientAppointmentsByDoctor(string patientID, string doctorID);

        Task<IEnumerable<Appointment>> GetAllDoctorAppointments(string doctorID);
    }
}
