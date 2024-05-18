using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

namespace PsychosocialSupportPlatformAPI.DataAccess.Statistics
{
    public interface IVideoStatisticsRepository
    {
        Task CreateVideoStatistics(VideoStatistics statistics);
        Task UpdateVideoStatistics(VideoStatistics statistics);
        Task DeleteVideoStatistics(int statisticsID);
        Task<IEnumerable<object>> GetAllVideoStatistics();
        Task<VideoStatistics> GetVideoStatisticsByID(int statisticsID);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName);
        Task<VideoStatistics> GetVideoStatisticsByPatientID(string patientID);
        Task<VideoStatistics> GetPatientVideoStatisticsByVideoID(string patientID, int videoID);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, string doctorId);


    }
}
