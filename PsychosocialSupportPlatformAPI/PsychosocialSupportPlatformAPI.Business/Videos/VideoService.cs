using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Videos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public async Task DeleteVideo(string videoUrl)
        {
            await _videoRepository.DeleteVideo(videoUrl);
        }

        public async Task<List<GetVideoDTO>> GetAllVideos()
        {
            return _mapper.Map<List<GetVideoDTO>>(await _videoRepository.GetAllVideos());
        }


        public async Task UploadVideo(UploadVideoDTO uploadVideoDTO)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory() + "/UploadedVideos/");
            //string fileName = Path.GetFileName(uploadVideoDTO.File.FileName);
            if (Path.GetExtension(uploadVideoDTO.File.FileName) != "mp4")
            {
                throw new ArgumentOutOfRangeException();
            }
            string newFileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(uploadVideoDTO.File.FileName));


            string filePath = string.Concat($"{basePath}", newFileName);

            string url = $"{_configuration["Urls:DevBaseUrl"]}/UploadedVideos/{newFileName}";

            using (var stream = new FileStream(filePath, FileMode.Create))
                await uploadVideoDTO.File.CopyToAsync(stream);


            await _videoRepository.AddVideo(new Entity.Entities.Video
            {
                Url = url,
                Path = filePath,
                Description = uploadVideoDTO.Description,
                Title = uploadVideoDTO.Title,
            });
        }
    }
}
