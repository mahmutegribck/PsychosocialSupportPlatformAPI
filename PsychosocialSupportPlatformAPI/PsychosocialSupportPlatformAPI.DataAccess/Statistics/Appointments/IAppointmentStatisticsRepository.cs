using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.DataAccess.Statistics.Appointments
{
    public interface IAppointmentStatisticsRepository
    {
        Task AddAppointmentStatistics(AppointmentStatistics appointmentStatistics);
        Task UpdateAppointmentStatistics(AppointmentStatistics appointmentStatistics);
        Task DeleteAppointmentStatistics(AppointmentStatistics appointmentStatistics);
        Task<AppointmentStatistics?> GetAppointmentStatisticsById(int appointmentStatisticsId, string patientId, string doctorId);

        Task<IEnumerable<AppointmentStatistics>> GetAllPatientAppointmentStatisticsByDoctorId(string doctorId);
        Task<IEnumerable<AppointmentStatistics>> GetAllPatientAppointmentStatisticsByPatientId(string doctorId, string patientId);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatistics();
    }
}
