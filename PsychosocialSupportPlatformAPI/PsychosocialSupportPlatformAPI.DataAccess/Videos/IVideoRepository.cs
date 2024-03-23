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
        Task DeleteVideo(int videoID);
        Task<List<Video>> GetAllVideos();
        Task UpdateVideo(Video video);

    }
}
