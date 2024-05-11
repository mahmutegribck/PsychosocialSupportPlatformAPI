using PsychosocialSupportPlatformAPI.Business.Statistics.Videos.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Statistics.Videos
{
    public interface IVideoStatisticsService
    {
        Task AddVideoStatistics(string patientId, AddVideoStatisticsDTO addVideoStatisticsDTO);
        Task DeleteVideoStatistics(int statisticsId);
        Task<IEnumerable<object>> GetAllVideoStatistics();
        Task<GetVideoStatisticsDTO> GetVideoStatisticsByID(int statisticsId);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientID(string patientId);
        Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, string doctorId);

    }
}
