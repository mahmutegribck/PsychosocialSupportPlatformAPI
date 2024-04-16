using AutoMapper;
using Google.Apis.Util;
using PsychosocialSupportPlatformAPI.Business.Statistics.DTOs;
using PsychosocialSupportPlatformAPI.Business.Videos;
using PsychosocialSupportPlatformAPI.DataAccess.Statistics;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Statistics
{
    public class VideoStatisticsService : IVideoStatisticsService
    {
        private readonly IVideoStatisticsRepository _videoStatisticsRepository;
        private readonly IMapper _mapper;
        public VideoStatisticsService(IVideoStatisticsRepository videoStatisticsRepository, IMapper mapper)
        {
            _videoStatisticsRepository = videoStatisticsRepository;
            _mapper = mapper;
        }
        public async Task CreateVideoStatistics(string userID, CreateVideoStatisticsDTO createVideoStatisticsDTO)
        {
            var videoStatistics = _mapper.Map<VideoStatistics>(createVideoStatisticsDTO);
            videoStatistics.PatientId = userID;
            await _videoStatisticsRepository.CreateVideoStatistics(videoStatistics);
        }

        public async Task DeleteVideoStatistics(int statisticsID)
        {
            await _videoStatisticsRepository.DeleteVideoStatistics(statisticsID);
        }

        public async Task<IEnumerable<object>> GetAllVideoStatistics()
        {
            return await _videoStatisticsRepository.GetAllVideoStatistics();
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientID(string patientID)
        {
            return (await _videoStatisticsRepository.GetAllVideoStatisticsByPatientID(patientID));
        }

        public async Task<GetVideoStatisticsDTO> GetVideoStatisticsByID(int statisticsID)
        {
            return _mapper.Map<GetVideoStatisticsDTO>(await _videoStatisticsRepository.GetVideoStatisticsByID(statisticsID));
        }

        public async Task<GetVideoStatisticsDTO> GetVideoStatisticsByPatientID(string patientID)
        {
            return _mapper.Map<GetVideoStatisticsDTO>(await _videoStatisticsRepository.GetVideoStatisticsByPatientID(patientID));
        }

        public async Task UpdateVideoStatistics(string userID, UpdateVideoStatisticsDTO updateVideoStatisticsDTO)
        {
            var videoStatistics = _mapper.Map<VideoStatistics>(updateVideoStatisticsDTO);
            videoStatistics.PatientId = userID;

            await _videoStatisticsRepository.UpdateVideoStatistics(videoStatistics);
        }
    }
}
