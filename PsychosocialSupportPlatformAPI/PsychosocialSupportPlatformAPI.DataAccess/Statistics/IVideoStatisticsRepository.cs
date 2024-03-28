using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.DataAccess.Statistics
{
    public interface IVideoStatisticsRepository
    {
        Task CreateVideoStatistics(VideoStatistics statistics);
        Task UpdateVideoStatistics(VideoStatistics statistics);
        Task DeleteVideoStatistics(int statisticsID);
        Task<IEnumerable<VideoStatistics>> GetAllVideoStatistics();
        Task<VideoStatistics> GetVideoStatisticsByID(int statisticsID);
        Task<IEnumerable<VideoStatistics>> GetAllVideoStatisticsByPatientID(string patientID);
        Task<VideoStatistics> GetVideoStatisticsByPatientID(string patientID);

    }
}
