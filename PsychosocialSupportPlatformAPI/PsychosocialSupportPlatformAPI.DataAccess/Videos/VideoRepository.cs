using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.DataAccess.Videos
{
    public class VideoRepository : IVideoRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;
        public VideoRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }
        public async Task AddVideo(Video video)
        {
            await _context.Videos.AddAsync(video);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVideo(string videoUrl)
        {
            var deletedVideo = await _context.Videos.Where(v => v.Url == videoUrl).FirstOrDefaultAsync();
            if (deletedVideo != null)
            {
                File.Delete(deletedVideo.Path);
                _context.Videos.Remove(deletedVideo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Video>> GetAllVideos()
        {
            return await _context.Videos.ToListAsync();
        }
    }
}
