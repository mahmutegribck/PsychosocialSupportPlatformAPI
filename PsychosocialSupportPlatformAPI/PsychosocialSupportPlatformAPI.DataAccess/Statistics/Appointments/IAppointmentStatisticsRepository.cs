using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.DataAccess.Statistics.Appointments
{
    public interface IAppointmentStatisticsRepository
    {
        Task AddAppointmentStatistics(AppointmentStatistics appointmentStatistics, CancellationToken cancellationToken);
        Task UpdateAppointmentStatistics(AppointmentStatistics appointmentStatistics, CancellationToken cancellationToken);
        Task DeleteAppointmentStatistics(AppointmentStatistics appointmentStatistics, CancellationToken cancellationToken);
        Task<AppointmentStatistics?> GetAppointmentStatisticsById(int appointmentStatisticsId, string patientId, int appointmentScheduleId, string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByDoctorUserName(string doctorUserName, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByPatientUserName(string patientUserName, string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByPatientUserName(string patientUserName, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatistics(CancellationToken cancellationToken);
        Task<bool> CheckPatientAppointmentStatistics(int appointmentScheduleId, CancellationToken cancellationToken);
    }
}
