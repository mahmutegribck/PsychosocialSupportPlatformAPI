﻿using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules
{
    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;
        public DoctorScheduleRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }
        public async Task CreateDoctorSchedule(DoctorSchedule doctorSchedule)
        {
            await _context.DoctorSchedules.AddAsync(doctorSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorSchedule(int doctorScheduleId)
        {
            var deleteDoctorSchedule = await _context.DoctorSchedules.FindAsync(doctorScheduleId) ?? throw new Exception();
            _context.DoctorSchedules.Remove(deleteDoctorSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DoctorSchedule>> GetAllDoctorSchedule()
        {
            return await _context.DoctorSchedules.ToListAsync();
        }

        public async Task<IEnumerable<DoctorSchedule>> GetAllDoctorScheduleById(string doctorId)
        {
            return await _context.DoctorSchedules.AsNoTracking().Where(s => s.DoctorId == doctorId).ToListAsync();
        }

        public async Task<DoctorSchedule> GetDoctorSchedule(string doctorId, DoctorSchedule doctorSchedule)
        {
            return await _context.DoctorSchedules.Where(s => s.DoctorId == doctorId && s.Day == doctorSchedule.Day).FirstOrDefaultAsync();
        }

        public async Task<DoctorSchedule> GetDoctorScheduleById(string doctorId, int scheduleId)
        {
            return await _context.DoctorSchedules.Where(s => s.DoctorId == doctorId && s.Id == scheduleId).FirstOrDefaultAsync();
        }

        public async Task UpdateDoctorSchedule(DoctorSchedule doctorSchedule)
        {
            var updateDoctorSchedule = await _context.DoctorSchedules.AsNoTracking().Where(s => s.Id == doctorSchedule.Id && s.DoctorId == doctorSchedule.DoctorId).FirstOrDefaultAsync() ?? throw new Exception();
            _context.DoctorSchedules.Update(doctorSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task<DoctorSchedule> GetDoctorScheduleByTimeRange(string doctorId, TimeRange timeRange, DayOfWeek day)
        {
            return await _context.DoctorSchedules.Where(s => s.DoctorId == doctorId && GetTimeRangeProperty(s, timeRange) && s.Day == day).FirstOrDefaultAsync();
        }

        private bool GetTimeRangeProperty(DoctorSchedule schedule, TimeRange timeRange)
        {
            switch (timeRange)
            {
                case TimeRange.EightToNine:
                    return schedule.EightToNine;
                case TimeRange.NineToTen:
                    return schedule.NineToTen;
                case TimeRange.TenToEleven:
                    return schedule.TenToEleven;
                case TimeRange.ElevenToTwelve:
                    return schedule.ElevenToTwelve;
                case TimeRange.TwelveToThirteen:
                    return schedule.TwelveToThirteen;
                case TimeRange.ThirteenToFourteen:
                    return schedule.ThirteenToFourteen;
                case TimeRange.FourteenToFifteen:
                    return schedule.FourteenToFifteen;
                case TimeRange.FifteenToSixteen:
                    return schedule.FifteenToSixteen;
                case TimeRange.SixteenToSeventeen:
                    return schedule.SixteenToSeventeen;
                default:
                    return false; // Handle any other cases if needed
            }

        }
    }
}
