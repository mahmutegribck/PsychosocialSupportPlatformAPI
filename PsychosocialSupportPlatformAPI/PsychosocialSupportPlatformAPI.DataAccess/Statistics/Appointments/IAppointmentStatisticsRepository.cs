using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.DataAccess.Statistics.Appointments
{
    public interface IAppointmentStatisticsRepository
    {
        Task AddAppointmentStatistics(AppointmentStatistics appointmentStatistics);
        Task UpdateAppointmentStatistics(AppointmentStatistics appointmentStatistics);
        Task DeleteAppointmentStatistics(AppointmentStatistics appointmentStatistics);
        Task<AppointmentStatistics?> GetAppointmentStatisticsById(int appointmentStatisticsId, string patientId, int appointmentScheduleId, string doctorId);

        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByDoctorUserName(string doctorUserName);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByPatientUserName(string patientUserName, string doctorId);
        Task<IEnumerable<AppointmentStatistics>> GetAllPatientAppointmentStatisticsByPatientUserName(string patientUserName);

        Task<IEnumerable<object>> GetAllPatientAppointmentStatistics();

        Task<bool> CheckPatientAppointmentStatistics(int appointmentScheduleId);
    }
}
