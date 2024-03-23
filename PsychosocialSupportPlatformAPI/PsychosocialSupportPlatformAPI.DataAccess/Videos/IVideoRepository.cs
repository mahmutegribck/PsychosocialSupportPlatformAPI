using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.DataAccess.Videos
{
    public interface IVideoRepository
    {
        Task AddVideo(Video video);
        Task DeleteVideo(string videoUrl);
        Task<List<Video>> GetAllVideos();
    }
}
