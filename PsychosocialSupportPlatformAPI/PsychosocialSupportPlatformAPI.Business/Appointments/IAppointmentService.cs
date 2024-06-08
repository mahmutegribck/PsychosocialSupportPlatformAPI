using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs;
using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Appointments
{
    public interface IAppointmentService
    {
        Task<IEnumerable<GetPatientDoctorDto>> GetPatientDoctorsByPatientId(string patientId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetPatientAppointmentsByPatientId(string patientId, CancellationToken cancellationToken);
        Task<GetPatientAppointmentDTO?> GetPatientAppointmentById(int patientAppointmentId, string patientId, CancellationToken cancellationToken);
        Task CancelPatientAppointment(CancelPatientAppointmentDTO cancelPatientAppointmentDTO, string patientId, CancellationToken cancellationToken);
        Task<bool> MakeAppointment(string patientId, MakeAppointmentDTO makeAppointmentDTO, CancellationToken cancellationToken);
        Task CreateAppointmentForPatient(string doctorId, CreateAppointmentForPatientDTO createAppointmentForPatientDTO, CancellationToken cancellationToken);
        Task CancelDoctorAppointment(CancelDoctorAppointmentDTO cancelDoctorAppointmentDTO, string doctorId, CancellationToken cancellationToken);

    }
}
