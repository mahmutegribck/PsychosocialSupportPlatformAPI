using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.DataAccess.Appointments
{
    public interface IAppointmentRepository
    {
        Task<AppointmentSchedule?> GetPatientAppointment(AppointmentSchedule appointmentSchedule);
        Task<AppointmentSchedule?> GetPatientAppointmentById(int patientAppointmentId, string patientId);
        Task<IEnumerable<object>> GetPatientAppointmentsByPatientId(string patientId);
        Task CancelPatientAppointment(AppointmentSchedule appointmentSchedule);

    }
}
