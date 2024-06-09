using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules
{
    public interface IAppointmentScheduleRepository
    {
        Task AddAppointmentScheduleList(List<AppointmentSchedule> appointmentSchedules, CancellationToken cancellationToken);
        Task AddAppointmentSchedule(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken);
        Task DeleteAppointmentScheduleList(IEnumerable<AppointmentSchedule> appointmentSchedules, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day, string patientId, CancellationToken cancellationToken);
        Task<IEnumerable<AppointmentSchedule>> GetAppointmentScheduleByDay(string doctorId, DateTime day, CancellationToken cancellationToken);
        Task<AppointmentSchedule?> GetAppointmentScheduleByDayAndTimeRange(string doctorId, DateTime day, TimeRange timeRange, CancellationToken cancellationToken);
        Task UpdateAppointmentSchedule(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken);
        Task<IEnumerable<AppointmentSchedule>> AllDoctorAppointments(string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<AppointmentSchedule>> GetAllDoctorAppointmentsByPatientId(string patientId, string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<AppointmentSchedule>> GetAllPastDoctorAppointmentsByPatientSlug(string patientSlug, string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<AppointmentSchedule>> GetAllDoctorAppointmentsByDate(DateTime day, string doctorId, CancellationToken cancellationToken);
        Task<AppointmentSchedule?> GetDoctorAppointmentByDateAndTimeRange(AppointmentSchedule appointmentSchedule, CancellationToken cancellationToken);

    }
}
