using PsychosocialSupportPlatformAPI.Business.Statistics.Appointments.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Statistics.Appointments
{
    public interface IAppointmentStatisticsService
    {
        Task AddAppointmentStatistics(AddAppointmentStatisticsDTO addAppointmentStatisticsDTO, string doctorId);
        Task UpdateAppointmentStatistics(UpdateAppointmentStatisticsDTO updateAppointmentStatisticsDTO, string doctorId);
        Task DeleteAppointmentStatistics(DeleteAppointmentStatisticsDTO deleteAppointmentStatisticsDTO, string doctorId);
        Task<IEnumerable<object>> GetAllPatientAppointmentStatisticsByDoctorId(string doctorId);
        Task<IEnumerable<GetAppointmentStatisticsDTO>> GetAllPatientAppointmentStatisticsByPatientId(string doctorId, string patientId);
        Task<IEnumerable<GetAppointmentStatisticsDTO>> GetAllPatientAppointmentStatisticsByPatientId(string patientId);

        Task<IEnumerable<object>> GetAllPatientAppointmentStatistics();


    }
}
