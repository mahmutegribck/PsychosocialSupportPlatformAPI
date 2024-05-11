using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.Statistics.Videos.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Statistics;
using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

namespace PsychosocialSupportPlatformAPI.Business.Statistics.Videos
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
        public async Task AddVideoStatistics(string patientId, AddVideoStatisticsDTO addVideoStatisticsDTO)
        {
            var videoStatistics = _mapper.Map<VideoStatistics>(addVideoStatisticsDTO);
            videoStatistics.PatientId = patientId;

            var existingVideoStatistics = await _videoStatisticsRepository.GetPatientVideoStatisticsByVideoID(patientId, videoStatistics.VideoId);

            if (existingVideoStatistics == null)
            {
                await _videoStatisticsRepository.CreateVideoStatistics(videoStatistics);
            }
            else
            {
                videoStatistics.Id = existingVideoStatistics.Id;
                videoStatistics.ClicksNumber = existingVideoStatistics.ClicksNumber + 1;

                await _videoStatisticsRepository.UpdateVideoStatistics(videoStatistics);
            }
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
            return await _videoStatisticsRepository.GetAllVideoStatisticsByPatientID(patientID);
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, string doctorId)
        {
            return await _videoStatisticsRepository.GetAllVideoStatisticsByPatientUserName(patientUserName, doctorId);
        }

        public async Task<GetVideoStatisticsDTO> GetVideoStatisticsByID(int statisticsID)
        {
            return _mapper.Map<GetVideoStatisticsDTO>(await _videoStatisticsRepository.GetVideoStatisticsByID(statisticsID));
        }

        public async Task<GetVideoStatisticsDTO> GetVideoStatisticsByPatientID(string patientID)
        {
            return _mapper.Map<GetVideoStatisticsDTO>(await _videoStatisticsRepository.GetVideoStatisticsByPatientID(patientID));
        }
    }
}
