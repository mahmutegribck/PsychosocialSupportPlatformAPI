using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

namespace PsychosocialSupportPlatformAPI.DataAccess.Videos
{
    public interface IVideoRepository
    {
        Task AddVideo(Video video, CancellationToken cancellationToken);
        Task DeleteVideo(int videoID, CancellationToken cancellationToken);
        Task<Video?> GetVideoByVideoSlug(string videoSlug, CancellationToken cancellationToken);
        Task<IEnumerable<Video>> GetAllVideos(CancellationToken cancellationToken);
        Task UpdateVideo(Video video, CancellationToken cancellationToken);

    }
}
