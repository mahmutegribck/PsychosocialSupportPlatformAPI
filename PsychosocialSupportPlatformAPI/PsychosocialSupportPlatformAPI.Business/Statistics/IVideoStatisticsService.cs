using PsychosocialSupportPlatformAPI.Business.Statistics.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Statistics
{
    public interface IVideoStatisticsService
    {
        Task AddVideoStatistics(string userID, AddVideoStatisticsDTO addVideoStatisticsDTO);
        Task UpdateVideoStatistics(string userID, UpdateVideoStatisticsDTO updateVideoStatisticsDTO);
        Task DeleteVideoStatistics(int statisticsID);
        Task<IEnumerable<object>> GetAllVideoStatistics();
        Task<GetVideoStatisticsDTO> GetVideoStatisticsByID(int statisticsID);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientID(string patientID);
        Task<GetVideoStatisticsDTO> GetVideoStatisticsByPatientID(string patientID);

    }
}
