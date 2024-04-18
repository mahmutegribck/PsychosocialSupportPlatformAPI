using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules
{
    public class AppointmentScheduleRepository : IAppointmentScheduleRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;
        public AppointmentScheduleRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }

        public async Task AddAppointmentSchedule(AppointmentSchedule appointmentSchedule)
        {
            await _context.AppointmentSchedules.AddAsync(appointmentSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task AddAppointmentScheduleList(List<AppointmentSchedule> appointmentSchedules)
        {
            await _context.AppointmentSchedules.AddRangeAsync(appointmentSchedules);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentSchedule(AppointmentSchedule appointmentSchedule)
        {
            _context.AppointmentSchedules.Remove(appointmentSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<object>> GetAllAppointmentSchedules()
        {
            var mergedSchedules = await _context.AppointmentSchedules.OrderBy(a => a.Day).ThenBy(a => a.TimeRange).Select(a => new 
            {
                a.Day,
                a.TimeRange,
                a.Status,
                DoctorName = a.Doctor.Name,
                DoctorSurname = a.Doctor.Surname,
                DoctorTitle = a.Doctor.Title 
            }).ToListAsync();

            var groupedSchedules = mergedSchedules
                .GroupBy(a => a.Day)
                .Select(group => new
                {
                    Day = group.Key,
                    Appointments = group.Select(a => new
                    {
                        a.TimeRange,
                        a.Status,
                        a.DoctorName,
                        a.DoctorSurname,
                        a.DoctorTitle
                    })
                });

            return groupedSchedules;


        }

        public async Task<object> GetAllAppointmentSchedulesByDoctor()
        {
            return await _context.AppointmentSchedules.Select(a => new
            {

                Doctor = a.Appointments.Select(d => new
                {
                    DoctorId = d.DoctorId,
                    DoctorName = d.Doctor.Name,
                    DoctorSurname = d.Doctor.Surname,
                    DoctorTitle = d.Doctor.Title,
                    DoctorProfileImage = d.Doctor.ProfileImageUrl,
                    DoctorSchedule = a.Doctor.DoctorSchedules.Select(ds => new
                    {
                        DoctorScheduleId = ds.Id,
                        ds.EightToNine,
                        ds.NineToTen,
                        ds.TenToEleven,
                        ds.ElevenToTwelve,
                        ds.TwelveToThirteen,
                        ds.ThirteenToFourteen,
                        ds.FourteenToFifteen,
                        ds.FifteenToSixteen,
                        ds.SixteenToSeventeen
                    })
                }),
            }).ToListAsync();
        }

        public Task<AppointmentSchedule> GetAllAppointmentSchedulesByTimeRange()
        {
            throw new NotImplementedException();
        }

        public async Task<AppointmentSchedule?> GetAppointmentScheduleByTimeRange(string doctorId, TimeRange timeRange, DateTime day)
        {

            return await _context.AppointmentSchedules.Where(a => a.Status == false && a.DoctorId == doctorId && a.TimeRange == timeRange && a.Day == day).FirstOrDefaultAsync();
        }

        public async Task UpdateAppointmentSchedule(AppointmentSchedule appointmentSchedule)
        {
            _context.AppointmentSchedules.Update(appointmentSchedule);
            await _context.SaveChangesAsync();
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
