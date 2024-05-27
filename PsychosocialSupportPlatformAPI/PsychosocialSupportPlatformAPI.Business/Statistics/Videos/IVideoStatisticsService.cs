using PsychosocialSupportPlatformAPI.Business.Statistics.Videos.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Statistics.Videos
{
    public interface IVideoStatisticsService
    {
        Task AddVideoStatistics(string patientId, AddVideoStatisticsDTO addVideoStatisticsDTO);
        Task DeleteVideoStatistics(int statisticsId);
        Task<IEnumerable<object>> GetAllVideoStatistics(CancellationToken cancellationToken);
        Task<GetVideoStatisticsDTO> GetVideoStatisticsByID(int statisticsId);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientId(string patientId);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, string doctorId);

    }
}
