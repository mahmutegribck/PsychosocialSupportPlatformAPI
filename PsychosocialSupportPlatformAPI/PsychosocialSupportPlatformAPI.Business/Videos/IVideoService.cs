using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Videos
{
    public interface IVideoService
    {
        Task UploadVideo(UploadVideoDTO uploadVideoDTO);
        Task DeleteVideo(int videoID);
        Task<GetVideoDTO> GetVideoById(int videoID);
        Task<List<GetVideoDTO>> GetAllVideos();
        Task UpdateVideo(UpdateVideoDTO updateVideoDTO);
    }
}
