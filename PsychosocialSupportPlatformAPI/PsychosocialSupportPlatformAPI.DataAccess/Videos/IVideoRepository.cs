using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

namespace PsychosocialSupportPlatformAPI.DataAccess.Videos
{
    public interface IVideoRepository
    {
        Task AddVideo(Video video);
        Task DeleteVideo(int videoID);
        Task<Video?> GetVideoByVideoSlug(string videoSlug);
        Task<List<Video>> GetAllVideos();
        Task UpdateVideo(Video video);

    }
}
