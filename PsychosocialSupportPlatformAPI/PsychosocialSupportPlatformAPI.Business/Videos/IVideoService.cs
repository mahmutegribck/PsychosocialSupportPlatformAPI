using Microsoft.IdentityModel.Protocols;
using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Videos
{
    public interface IVideoService
    {
        Task UploadVideo(UploadVideoDTO uploadVideoDTO);

        Task DeleteVideo(int videoID);

        Task<List<GetVideoDTO>> GetAllVideos();
    }
}
