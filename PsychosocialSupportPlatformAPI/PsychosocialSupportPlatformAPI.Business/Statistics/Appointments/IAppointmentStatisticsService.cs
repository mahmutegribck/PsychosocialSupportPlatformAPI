using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Statistics.Appointments
{
    public interface IAppointmentStatisticsService
    {
        Task AddAppointmentStatistics(AddAppointmentStatisticsDTO addAppointmentStatisticsDTO, string doctorId, CancellationToken cancellationToken);
        Task UpdateAppointmentStatistics(UpdateAppointmentStatisticsDTO updateAppointmentStatisticsDTO, string doctorId, CancellationToken cancellationToken);
        Task DeleteAppointmentStatistics(DeleteAppointmentStatisticsDTO deleteAppointmentStatisticsDTO, string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByDoctorUserName(string doctorUserName, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByPatientUserName(string patientUserName, string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByPatientUserName(string patientUserName, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatistics(CancellationToken cancellationToken);

    }
}
