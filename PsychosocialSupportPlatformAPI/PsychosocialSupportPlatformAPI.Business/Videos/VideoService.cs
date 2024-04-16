using AutoMapper;
using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Videos;
using PsychosocialSupportPlatformAPI.Entity.Entities;

namespace PsychosocialSupportPlatformAPI.Business.Videos
{
    public class VideoService : IVideoService
    {
        private readonly IConfiguration _configuration;
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;
        public VideoService(IConfiguration configuration, IVideoRepository videoRepository, IMapper mapper)
        {
            _configuration = configuration;
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task DeleteVideo(int videoID)
        {
            await _videoRepository.DeleteVideo(videoID);
        }

        public async Task<List<GetVideoDTO>> GetAllVideos()
        {
            return _mapper.Map<List<GetVideoDTO>>(await _videoRepository.GetAllVideos());
        }

        public async Task<GetVideoDTO> GetVideoById(int videoID)
        {
            return _mapper.Map<GetVideoDTO>(await _videoRepository.GetVideoById(videoID));
        }

        public async Task UpdateVideo(UpdateVideoDTO updateVideoDTO)
        {
            await _videoRepository.UpdateVideo(_mapper.Map<Video>(updateVideoDTO));
        }

        public async Task UploadVideo(UploadVideoDTO uploadVideoDTO)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory() + "/UploadedVideos/");

            var extension = Path.GetExtension(uploadVideoDTO.File.FileName);
            if (extension == null || extension != ".mp4" && extension != ".MP4")
            {
                throw new ArgumentOutOfRangeException();
            }
            string newFileName = Path.ChangeExtension(Path.GetRandomFileName(), extension);


            string filePath = string.Concat($"{basePath}", newFileName);

            string url = $"{_configuration["Urls:DevBaseUrl"]}/UploadedVideos/{newFileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
                await uploadVideoDTO.File.CopyToAsync(stream);

            await _videoRepository.AddVideo(new Video
            {
                Url = url,
                Path = filePath,
                Description = uploadVideoDTO.Description,
                Title = uploadVideoDTO.Title,

            });
        }
    }
}
