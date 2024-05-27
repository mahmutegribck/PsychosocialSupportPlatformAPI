using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
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

        public async Task CreateDoctorScheduleList(List<DoctorSchedule> doctorSchedules)
        {
            await _context.DoctorSchedules.AddRangeAsync(doctorSchedules);
            await _context.SaveChangesAsync();
        }
        public async Task CreateDoctorSchedule(DoctorSchedule doctorSchedule)
        {
            await _context.DoctorSchedules.AddAsync(doctorSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorSchedule(DoctorSchedule doctorSchedule)
        {
            _context.DoctorSchedules.Update(doctorSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDoctorSchedule(DoctorSchedule doctorSchedule)
        {
            _context.DoctorSchedules.Remove(doctorSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task<DoctorSchedule?> GetDoctorScheduleByDay(string doctorId, DateTime day)
        {
            return await _context.DoctorSchedules.AsNoTracking().Where(s => s.DoctorId == doctorId && s.Day == day).FirstOrDefaultAsync();
        }

        public async Task<DoctorSchedule?> GetDoctorScheduleById(string doctorId, int scheduleId)
        {
            return await _context.DoctorSchedules.AsNoTracking().Where(s => s.DoctorId == doctorId && s.Id == scheduleId).FirstOrDefaultAsync();
        }

        public async Task<DoctorSchedule?> GetDoctorScheduleByTimeRange(string doctorId, TimeRange timeRange, DateTime day)
        {
            return await _context.DoctorSchedules.AsNoTracking().Where(s => s.DoctorId == doctorId && s.Day == day).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DoctorSchedule?>> GetAllDoctorScheduleById(string doctorId)
        {
            return await _context.DoctorSchedules.AsNoTracking().Where(s => s.DoctorId == doctorId).ToListAsync();
        }

        public async Task<IEnumerable<DoctorSchedule?>> GetAllDoctorSchedulesByDate(DateTime day,CancellationToken cancellationToken)
        {
            return await _context.DoctorSchedules
                .AsNoTracking()
                .Include(ds => ds.Doctor)
                .Where(ds => ds.Day == day).ToListAsync(cancellationToken);
        }
    }
}
