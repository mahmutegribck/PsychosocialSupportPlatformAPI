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
        public VideoStatisticsService(
            IVideoStatisticsRepository videoStatisticsRepository,
            IMapper mapper)
        {
            _videoStatisticsRepository = videoStatisticsRepository;
            _mapper = mapper;
        }
        public async Task AddVideoStatistics(string patientId, AddVideoStatisticsDTO addVideoStatisticsDTO, CancellationToken cancellationToken)
        {
            var videoStatistics = _mapper.Map<VideoStatistics>(addVideoStatisticsDTO);
            videoStatistics.PatientId = patientId;

            var existingVideoStatistics = await _videoStatisticsRepository.GetPatientVideoStatisticsByVideoId(patientId, videoStatistics.VideoId, cancellationToken);

            if (existingVideoStatistics == null)
            {
                await _videoStatisticsRepository.CreateVideoStatistics(videoStatistics, cancellationToken);
            }
            else
            {
                videoStatistics.Id = existingVideoStatistics.Id;
                videoStatistics.ClicksNumber = existingVideoStatistics.ClicksNumber + 1;

                await _videoStatisticsRepository.UpdateVideoStatistics(videoStatistics, cancellationToken);
            }
        }

        public async Task DeleteVideoStatistics(int statisticsId, CancellationToken cancellationToken)
        {
            await _videoStatisticsRepository.DeleteVideoStatistics(statisticsId, cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllVideoStatistics(CancellationToken cancellationToken)
        {
            return await _videoStatisticsRepository.GetAllVideoStatistics(cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientId(string patientId, CancellationToken cancellationToken)
        {
            return await _videoStatisticsRepository.GetAllVideoStatisticsByPatientId(patientId, cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, CancellationToken cancellationToken)
        {
            return await _videoStatisticsRepository.GetAllVideoStatisticsByPatientUserName(patientUserName, cancellationToken);
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, string doctorId, CancellationToken cancellationToken)
        {
            return await _videoStatisticsRepository.GetAllVideoStatisticsByPatientUserName(patientUserName, doctorId, cancellationToken);
        }

        public async Task<GetVideoStatisticsDTO> GetVideoStatisticsById(int statisticsId, CancellationToken cancellationToken)
        {
            return _mapper.Map<GetVideoStatisticsDTO>(await _videoStatisticsRepository.GetVideoStatisticsById(statisticsId, cancellationToken));
        }
    }
}
