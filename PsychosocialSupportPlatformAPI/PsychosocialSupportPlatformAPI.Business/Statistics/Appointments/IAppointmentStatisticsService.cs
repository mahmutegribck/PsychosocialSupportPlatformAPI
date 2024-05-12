using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Statistics.Appointments
{
    public interface IAppointmentStatisticsService
    {
        Task AddAppointmentStatistics(AddAppointmentStatisticsDTO addAppointmentStatisticsDTO, string doctorId);
        Task UpdateAppointmentStatistics(UpdateAppointmentStatisticsDTO updateAppointmentStatisticsDTO, string doctorId);
        Task DeleteAppointmentStatistics(DeleteAppointmentStatisticsDTO deleteAppointmentStatisticsDTO, string doctorId);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByDoctorUserName(string doctorUserName);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByPatientUserName(string patientUserName, string doctorId);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByPatientUserName(string patientUserName);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatistics();

    }
}
