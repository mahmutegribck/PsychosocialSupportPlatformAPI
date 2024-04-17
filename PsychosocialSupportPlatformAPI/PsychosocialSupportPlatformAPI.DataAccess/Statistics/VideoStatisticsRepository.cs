using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.DataAccess.Statistics
{
    public class VideoStatisticsRepository : IVideoStatisticsRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;
        public VideoStatisticsRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }
        public async Task CreateVideoStatistics(VideoStatistics statistics)
        {
            await _context.VideoStatistics.AddAsync(statistics);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVideoStatistics(int statisticsID)
        {
            var deleteVideoStatistics = await _context.VideoStatistics.FindAsync(statisticsID);

            if (deleteVideoStatistics != null)
            {
                _context.VideoStatistics.Remove(deleteVideoStatistics);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<object>> GetAllVideoStatistics()
        {

            return await _context.Videos.Select(v => new
            {
                VideoId = v.Id,
                VideoTitle = v.Title,
                VideoStatistics = v.Statistics.Select(s => new
                {
                    PatientNameId = s.PatientId,
                    PatientName = s.Patient.Name,
                    PatientSurname = s.Patient.Surname,
                    PatientProfileImageUrl = s.Patient.ProfileImageUrl,
                    VideoClicksNumber = s.ClicksNumber,
                    VideoViewingRate = s.ViewingRate
                }),

            }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientID(string patientID)
        {

            return await _context.Videos.Select(v => new
            {
                VideoId = v.Id,
                VideoTitle = v.Title,
                VideoStatistics = v.Statistics.Where(s => s.PatientId == patientID).Select(s => new
                {
                    PatientProfileImageUrl = s.Patient.ProfileImageUrl,
                    VideoClicksNumber = s.ClicksNumber,
                    VideoViewingRate = s.ViewingRate
                }),
            }).ToListAsync();
        }

        public async Task<VideoStatistics> GetVideoStatisticsByID(int statisticsID)
        {
            return await _context.VideoStatistics.Where(v => v.Id == statisticsID).FirstOrDefaultAsync();
        }

        public async Task<VideoStatistics> GetVideoStatisticsByPatientID(string patientID)
        {
            return await _context.VideoStatistics.Where(v => v.PatientId == patientID).FirstOrDefaultAsync();
        }

        public async Task UpdateVideoStatistics(VideoStatistics statistics)
        {
            _context.VideoStatistics.Update(statistics);
            await _context.SaveChangesAsync();

        }
    }
}
