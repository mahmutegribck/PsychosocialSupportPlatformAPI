using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

namespace PsychosocialSupportPlatformAPI.DataAccess.Videos
{
    public class VideoRepository : IVideoRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;
        public VideoRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }
        public async Task AddVideo(Video video, CancellationToken cancellationToken)
        {
            await _context.Videos.AddAsync(video, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteVideo(int videoID, CancellationToken cancellationToken)
        {
            var deletedVideo = await _context.Videos
                .AsNoTracking()
                .Where(v => v.Id == videoID)
                .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Video Bulunamadı");

            File.Delete(deletedVideo.Path);
            _context.Videos.Remove(deletedVideo);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Video>> GetAllVideos(CancellationToken cancellationToken)
        {
            return await _context.Videos.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Video?> GetVideoByVideoSlug(string videoSlug, CancellationToken cancellationToken)
        {
            return await _context.Videos.AsNoTracking().Where(v => v.VideoSlug == videoSlug).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateVideo(Video video, CancellationToken cancellationToken)
        {
            var updatedVideo = await _context.Videos.AsNoTracking().Where(v => v.Id == video.Id).FirstOrDefaultAsync(cancellationToken);
            if (updatedVideo != null)
            {
                updatedVideo.Title = video.Title;
                updatedVideo.Description = video.Description;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
