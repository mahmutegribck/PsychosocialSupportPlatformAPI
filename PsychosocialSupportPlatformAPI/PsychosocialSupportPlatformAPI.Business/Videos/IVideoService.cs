using PsychosocialSupportPlatformAPI.Business.Videos.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Videos
{
    public interface IVideoService
    {
        Task UploadVideo(UploadVideoDTO uploadVideoDTO, string rootPath, CancellationToken cancellationToken);
        Task DeleteVideo(int videoID, CancellationToken cancellationToken);
        Task<GetVideoDTO?> GetVideoByVideoSlug(string videoSlug, CancellationToken cancellationToken);
        Task<IEnumerable<GetVideoDTO>> GetAllVideos(CancellationToken cancellationToken);
        Task UpdateVideo(UpdateVideoDTO updateVideoDTO, CancellationToken cancellationToken);
    }
}
