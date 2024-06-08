using AutoMapper;
using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Videos;
using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;
using System.Text.RegularExpressions;

namespace PsychosocialSupportPlatformAPI.Business.Videos
{
    public class VideoService : IVideoService
    {
        private readonly IConfiguration _configuration;
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public VideoService(
            IConfiguration configuration,
            IVideoRepository videoRepository,
            IMapper mapper)
        {
            _configuration = configuration;
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task DeleteVideo(int videoID, CancellationToken cancellationToken)
        {
            await _videoRepository.DeleteVideo(videoID, cancellationToken);
        }

        public async Task<IEnumerable<GetVideoDTO>> GetAllVideos(CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<GetVideoDTO>>(await _videoRepository.GetAllVideos(cancellationToken));
        }

        public async Task<GetVideoDTO?> GetVideoByVideoSlug(string videoSlug, CancellationToken cancellationToken)
        {
            return _mapper.Map<GetVideoDTO?>(await _videoRepository.GetVideoByVideoSlug(videoSlug, cancellationToken));
        }

        public async Task UpdateVideo(UpdateVideoDTO updateVideoDTO, CancellationToken cancellationToken)
        {
            await _videoRepository.UpdateVideo(_mapper.Map<Video>(updateVideoDTO), cancellationToken);
        }

        public async Task UploadVideo(UploadVideoDTO uploadVideoDTO, string rootPath, CancellationToken cancellationToken)
        {
            string basePath = rootPath + "\\Videos\\";

            if (!System.IO.Directory.Exists(basePath))
            {
                System.IO.Directory.CreateDirectory(basePath);
            }

            string extension = Path.GetExtension(uploadVideoDTO.File.FileName);

            if (extension == null || extension != ".mp4" && extension != ".MP4")
            {
                throw new ArgumentOutOfRangeException("Video Uzantısı Geçersiz.");
            }

            char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
            char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

            string videoSlug = uploadVideoDTO.Title;
            for (int i = 0; i < turkishChars.Length; i++)
                videoSlug = videoSlug.Replace(turkishChars[i], englishChars[i]);

            videoSlug = Regex.Replace(videoSlug, @"[^a-zA-Z0-9-/?]", "-").Trim();

            string newVideoName = Path.ChangeExtension(videoSlug, extension);

            string videoPath = string.Concat($"{basePath}", newVideoName);
            string videoUrl = $"{_configuration["Urls:BaseUrl"]}/Videos/{newVideoName}";

            using (var stream = new FileStream(videoPath, FileMode.Create))
                await uploadVideoDTO.File.CopyToAsync(stream, cancellationToken);

            
            await _videoRepository.AddVideo(new Video
            {
                Url = videoUrl,
                Path = videoPath,
                Description = uploadVideoDTO.Description,
                Title = uploadVideoDTO.Title.Trim(),
                VideoSlug = videoSlug.ToLower() + "-" + new Random().Next(1000, 1000000)

            }, cancellationToken);
        }
    }
}
