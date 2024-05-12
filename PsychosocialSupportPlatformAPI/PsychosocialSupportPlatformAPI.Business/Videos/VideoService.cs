﻿using AutoMapper;
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

        public async Task<GetVideoDTO?> GetVideoByVideoSlug(string videoSlug)
        {
            return _mapper.Map<GetVideoDTO?>(await _videoRepository.GetVideoByVideoSlug(videoSlug));
        }

        public async Task UpdateVideo(UpdateVideoDTO updateVideoDTO)
        {
            await _videoRepository.UpdateVideo(_mapper.Map<Video>(updateVideoDTO));
        }

        public async Task UploadVideo(UploadVideoDTO uploadVideoDTO, string rootPath)
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

            string newVideoName = Path.ChangeExtension(Path.GetRandomFileName(), extension);

            string videoPath = string.Concat($"{basePath}", newVideoName);
            string videoUrl = $"{_configuration["Urls:DevBaseUrl"]}/Videos/{newVideoName}";

            using (var stream = new FileStream(videoPath, FileMode.Create))
                await uploadVideoDTO.File.CopyToAsync(stream);


            char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
            char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

            string videoSlug = "";
            for (int i = 0; i < turkishChars.Length; i++)
                videoSlug = uploadVideoDTO.Title.Replace(turkishChars[i], englishChars[i]);

            videoSlug = Regex.Replace(uploadVideoDTO.Title, @"[^a-zA-Z0-9]", "-").Trim();


            await _videoRepository.AddVideo(new Video
            {
                Url = videoUrl,
                Path = videoPath,
                Description = uploadVideoDTO.Description,
                Title = uploadVideoDTO.Title.Trim(),
                VideoSlug = videoSlug.ToLower() + "-" + new Random().Next(1000, 1000000)

            });
        }
    }
}
