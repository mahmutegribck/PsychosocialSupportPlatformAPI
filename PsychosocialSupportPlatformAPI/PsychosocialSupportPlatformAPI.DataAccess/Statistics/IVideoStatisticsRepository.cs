using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

namespace PsychosocialSupportPlatformAPI.DataAccess.Statistics
{
    public interface IVideoStatisticsRepository
    {
        Task CreateVideoStatistics(VideoStatistics statistics, CancellationToken cancellationToken);
        Task UpdateVideoStatistics(VideoStatistics statistics, CancellationToken cancellationToken);
        Task DeleteVideoStatistics(int statisticsId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllVideoStatistics(CancellationToken cancellationToken);
        Task<VideoStatistics?> GetVideoStatisticsById(int statisticsId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientId(string patientId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, CancellationToken cancellationToken);
        Task<VideoStatistics?> GetPatientVideoStatisticsByVideoId(string patientId, int videoId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, string doctorId, CancellationToken cancellationToken);

    }
}
