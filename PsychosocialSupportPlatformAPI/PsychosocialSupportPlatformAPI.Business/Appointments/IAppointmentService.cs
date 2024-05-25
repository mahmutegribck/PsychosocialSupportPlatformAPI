using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Appointments
{
    public interface IAppointmentService
    {
        Task<IEnumerable<GetPatientDoctorDto>> GetPatientDoctorsByPatientId(string patientId);
        Task<IEnumerable<object>> GetPatientAppointmentsByPatientId(string patientId);
        Task<GetPatientAppointmentDTO?> GetPatientAppointmentById(int patientAppointmentId, string patientId);
        Task CancelPatientAppointment(CancelPatientAppointmentDTO cancelPatientAppointmentDTO, string patientId);
        Task<bool> MakeAppointment(string patientId, MakeAppointmentDTO makeAppointmentDTO);
        Task CancelDoctorAppointment(CancelDoctorAppointmentDTO cancelDoctorAppointmentDTO, string doctorId);

    }
}
