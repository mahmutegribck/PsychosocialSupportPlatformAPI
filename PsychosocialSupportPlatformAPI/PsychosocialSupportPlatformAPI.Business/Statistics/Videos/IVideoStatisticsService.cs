using PsychosocialSupportPlatformAPI.Business.Statistics.Videos.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Statistics.Videos
{
    public interface IVideoStatisticsService
    {
        Task AddVideoStatistics(string patientId, AddVideoStatisticsDTO addVideoStatisticsDTO, CancellationToken cancellationToken);
        Task DeleteVideoStatistics(int statisticsId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllVideoStatistics(CancellationToken cancellationToken);
        Task<GetVideoStatisticsDTO> GetVideoStatisticsById(int statisticsId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientId(string patientId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, string doctorId, CancellationToken cancellationToken);

    }
}
