using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.VideoStatistics.Select(vs => new
            {
                vs.Id,
                vs.ViewingRate,
                vs.ClicksNumber,
                vs.PatientId,
                vs.VideoId,
                vs.Video.Title

            }).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientID(string patientID)
        {

            return await _context.VideoStatistics.Where(vs => vs.PatientId == patientID).Select(vs => new
            {
                vs.Id,
                vs.ViewingRate,
                vs.ClicksNumber,
                vs.VideoId,
                vs.Video.Title

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

            //var updateVideoStatistics = await _context.VideoStatistics.FindAsync(statistics.Id);

            //updateVideoStatistics.ViewingRate = statistics.ViewingRate;
            //updateVideoStatistics.ClicksNumber = statistics.ClicksNumber;
            //_context.VideoStatistics.Update(updateVideoStatistics);
            //await _context.SaveChangesAsync();
        }
    }
}
