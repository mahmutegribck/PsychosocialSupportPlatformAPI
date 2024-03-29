﻿using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<VideoStatistics>> GetAllVideoStatistics()
        {
            return await _context.VideoStatistics.ToListAsync();
        }

        public async Task<IEnumerable<VideoStatistics>> GetAllVideoStatisticsByPatientID(string patientID)
        {
            return await _context.VideoStatistics.Where(v => v.PatientId == patientID).ToListAsync();
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
