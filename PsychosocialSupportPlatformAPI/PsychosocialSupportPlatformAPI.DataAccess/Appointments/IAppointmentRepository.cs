using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.DataAccess.Appointments
{
    public interface IAppointmentRepository
    {
        Task<AppointmentSchedule?> GetPatientAppointment(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken);
        Task<AppointmentSchedule?> GetDoctorAppointment(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken);
        Task<AppointmentSchedule?> GetPatientAppointmentById(int patientAppointmentId, string patientId, CancellationToken cancellationToken);
        Task<IEnumerable<Doctor>> GetPatientDoctorsByPatientId(string patientId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetPatientAppointmentsByPatientId(string patientId, CancellationToken cancellationToken);
        Task<AppointmentSchedule?> GetPatientLastAppointment(string patientId, CancellationToken cancellationToken);
        Task CancelPatientAppointment(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken);
        Task CancelDoctorAppointment(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken);
        Task<bool> CheckPatientAppointment(int appointmentScheduleId, string patientId, string doctorId, CancellationToken cancellationToken);

    }
}
