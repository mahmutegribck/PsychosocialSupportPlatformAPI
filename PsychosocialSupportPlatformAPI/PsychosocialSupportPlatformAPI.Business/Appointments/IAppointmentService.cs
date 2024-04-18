using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Appointments
{
    public interface IAppointmentService
    {
        Task CreatePatientAppointment(CreateAppointmentDTO createAppointmentDTO);
        Task DeletePatientAppointment(int appointmentID, string patientID);
        Task<GetAppointmentDTO> GetPatientAppointmentById(int appointmentID, string patientID);
        Task<GetAppointmentDTO> GetDoctorAppointmentById(int appointmentID, string doctorID);
        Task<IEnumerable<GetAppointmentDTO>> GetAllPatientAppointments(string patientID);
        Task<IEnumerable<GetAppointmentDTO>> GetAllDoctorAppointments(string doctorID);
    }
}
