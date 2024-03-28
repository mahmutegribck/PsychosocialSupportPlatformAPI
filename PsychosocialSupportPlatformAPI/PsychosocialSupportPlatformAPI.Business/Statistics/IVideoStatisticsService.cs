using PsychosocialSupportPlatformAPI.Business.Statistics.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Statistics
{
    public interface IVideoStatisticsService
    {
        Task CreateVideoStatistics(CreateVideoStatisticsDTO createVideoStatisticsDTO);
        Task UpdateVideoStatistics(UpdateVideoStatisticsDTO updateVideoStatisticsDTO);
        Task DeleteVideoStatistics(int statisticsID);
        Task<IEnumerable<GetVideoStatisticsDTO>> GetAllVideoStatistics();
        Task<GetVideoStatisticsDTO> GetVideoStatisticsByID(int statisticsID);
        Task<IEnumerable<GetVideoStatisticsDTO>> GetAllVideoStatisticsByPatientID(string patientID);
        Task<GetVideoStatisticsDTO> GetVideoStatisticsByPatientID(string patientID);

    }
}
