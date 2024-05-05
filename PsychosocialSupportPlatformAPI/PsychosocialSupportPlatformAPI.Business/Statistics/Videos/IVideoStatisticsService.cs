using PsychosocialSupportPlatformAPI.Business.Statistics.Videos.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Statistics.Videos
{
    public interface IVideoStatisticsService
    {
        Task AddVideoStatistics(string userID, AddVideoStatisticsDTO addVideoStatisticsDTO);
        Task DeleteVideoStatistics(int statisticsID);
        Task<IEnumerable<object>> GetAllVideoStatistics();
        Task<GetVideoStatisticsDTO> GetVideoStatisticsByID(int statisticsID);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientID(string patientID);
        Task<GetVideoStatisticsDTO> GetVideoStatisticsByPatientID(string patientID);

    }
}
