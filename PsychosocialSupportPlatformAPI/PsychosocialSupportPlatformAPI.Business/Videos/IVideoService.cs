using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Videos
{
    public interface IVideoService
    {
        Task UploadVideo(UploadVideoDTO uploadVideoDTO, string rootPath);
        Task DeleteVideo(int videoID);
        Task<GetVideoDTO?> GetVideoByVideoSlug(string videoSlug);
        Task<List<GetVideoDTO>> GetAllVideos();
        Task UpdateVideo(UpdateVideoDTO updateVideoDTO);
    }
}
