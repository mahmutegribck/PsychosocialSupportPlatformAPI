﻿using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Videos;

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

            //var deneme = await _context.AppointmentSchedules
            //    .Include(a => a.Patient)
            //    .Where(a => a.DoctorId == doctorId && a.PatientId != null)
            //    .GroupBy(a => a.Patient)
            //    .Select(group => new
            //    {
            //        PatientId = group.Key.Id,
            //        PatientName = group.Key.Name,
            //        PatientSurname = group.Key.Surname,
            //        PatientProfileImageUrl = group.Key.ProfileImageUrl,


            //        VideoStatistics = group
            //            .SelectMany(s => s.Patient.Statistics)
            //            .Where(s => s.VideoId != null)
            //            .Select(s => new
            //            {
            //                VideoId = s.VideoId,
            //                VideoTitle = s.Video.Title,
            //                VideoClicksNumber = s.ClicksNumber,
            //                VideoViewingRate = s.ViewingRate
            //            }).Distinct()

            //    }).ToListAsync();
            //return deneme;
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientId(string patientId)
        {
            return await _context.Videos.Select(v => new
            {
                VideoId = v.Id,
                VideoTitle = v.Title,
                VideoStatistics = v.Statistics.Where(s => s.PatientId == patientId).Select(s => new
                {
                    PatientProfileImageUrl = s.Patient.ProfileImageUrl,
                    VideoClicksNumber = s.ClicksNumber,
                    VideoViewingRate = s.ViewingRate
                }),
            }).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName)
        {
            return await _context.Videos.Include(v => v.Statistics).ThenInclude(v => v.Patient).Select(v => new
            {
                VideoId = v.Id,
                VideoTitle = v.Title,
                VideoStatistics = v.Statistics.Where(s => s.Patient.UserName == patientUserName).Select(s => new
                {
                    PatientProfileImageUrl = s.Patient.ProfileImageUrl,
                    VideoClicksNumber = s.ClicksNumber,
                    VideoViewingRate = s.ViewingRate
                }),
            }).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<object>> GetAllVideoStatisticsByPatientUserName(string patientUserName, string doctorId)
        {
            return await _context.Patients.Where(p => p.UserName == patientUserName).Select(p => new
            {
                PatientName = p.Name,
                PatientSurname = p.Surname,
                PatientProfileImageUrl = p.ProfileImageUrl,

                Videos = _context.Videos.Select(v => new
                {
                    VideoId = v.Id,
                    VideoTitle = v.Title,
                    VideoStatistics = p.Statistics.Where(s => s.VideoId == v.Id).Select(s => new
                    {
                        VideoClicksNumber = s.ClicksNumber,
                        VideoViewingRate = s.ViewingRate
                    }).ToList(),
                }).ToList(),

            }).AsNoTracking().ToListAsync();

        }

        public async Task<VideoStatistics> GetPatientVideoStatisticsByVideoID(string patientID, int videoID)
        {
            return await _context.VideoStatistics.AsNoTracking().Where(v => v.VideoId == videoID && v.PatientId == patientID).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<VideoStatistics> GetVideoStatisticsByID(int statisticsID)
        {
            return await _context.VideoStatistics.Where(v => v.Id == statisticsID).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<VideoStatistics> GetVideoStatisticsByPatientID(string patientID)
        {
            return await _context.VideoStatistics.Where(v => v.PatientId == patientID).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task UpdateVideoStatistics(VideoStatistics statistics)
        {
            _context.VideoStatistics.Update(statistics);
            await _context.SaveChangesAsync();

        }
    }
}
