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
        public async Task CreateVideoStatistics(CreateVideoStatisticsDTO createVideoStatisticsDTO)
        {
            await _videoStatisticsRepository.CreateVideoStatistics(_mapper.Map<VideoStatistics>(createVideoStatisticsDTO));
        }

        public async Task DeleteVideoStatistics(int statisticsID)
        {
            await _videoStatisticsRepository.DeleteVideoStatistics(statisticsID);
        }

        public async Task<IEnumerable<GetVideoStatisticsDTO>> GetAllVideoStatistics()
        {
            return _mapper.Map<List<GetVideoStatisticsDTO>>(await _videoStatisticsRepository.GetAllVideoStatistics());
        }

        public async Task<IEnumerable<GetVideoStatisticsDTO>> GetAllVideoStatisticsByPatientID(string patientID)
        {
            return _mapper.Map<List<GetVideoStatisticsDTO>>(await _videoStatisticsRepository.GetAllVideoStatisticsByPatientID(patientID));
        }

        public async Task<GetVideoStatisticsDTO> GetVideoStatisticsByID(int statisticsID)
        {
            return _mapper.Map<GetVideoStatisticsDTO>(await _videoStatisticsRepository.GetVideoStatisticsByID(statisticsID));
        }

        public async Task<GetVideoStatisticsDTO> GetVideoStatisticsByPatientID(string patientID)
        {
            return _mapper.Map<GetVideoStatisticsDTO>(await _videoStatisticsRepository.GetVideoStatisticsByPatientID(patientID));
        }

        public async Task UpdateVideoStatistics(UpdateVideoStatisticsDTO updateVideoStatisticsDTO)
        {
            await _videoStatisticsRepository.UpdateVideoStatistics(_mapper.Map<VideoStatistics>(updateVideoStatisticsDTO));
        }
    }
}
