using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.DataAccess.Appointments
{
    public interface IAppointmentRepository
    {
        Task<AppointmentSchedule?> GetPatientAppointment(AppointmentSchedule appointmentSchedule);
        Task<AppointmentSchedule?> GetDoctorAppointment(AppointmentSchedule appointmentSchedule);
        Task<AppointmentSchedule?> GetPatientAppointmentById(int patientAppointmentId, string patientId);
        Task<IEnumerable<Doctor>> GetPatientDoctorsByPatientId(string patientId);
        Task<IEnumerable<object>> GetPatientAppointmentsByPatientId(string patientId);
        Task<AppointmentSchedule?> GetPatientLastAppointment(string patientId);
        Task CancelPatientAppointment(AppointmentSchedule appointmentSchedule);
        Task CancelDoctorAppointment(AppointmentSchedule appointmentSchedule);
        Task<bool> CheckPatientAppointment(int appointmentScheduleId, string patientId, string doctorId, CancellationToken cancellationToken);

    }
}
