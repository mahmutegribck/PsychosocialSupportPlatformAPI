using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules
{
    public class DoctorScheduleRepository : IDoctorScheduleRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;
        public DoctorScheduleRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }

        public async Task CreateDoctorScheduleList(List<DoctorSchedule> doctorSchedules, CancellationToken cancellationToken)
        {
            await _context.DoctorSchedules.AddRangeAsync(doctorSchedules, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateDoctorSchedule(DoctorSchedule doctorSchedule, CancellationToken cancellationToken)
        {
            await _context.DoctorSchedules.AddAsync(doctorSchedule, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateDoctorSchedule(DoctorSchedule doctorSchedule, CancellationToken cancellationToken)
        {
            _context.DoctorSchedules.Update(doctorSchedule);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteDoctorSchedule(DoctorSchedule doctorSchedule, CancellationToken cancellationToken)
        {
            _context.DoctorSchedules.Remove(doctorSchedule);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<DoctorSchedule?> GetDoctorScheduleByDay(string doctorId, DateTime day, CancellationToken cancellationToken)
        {
            return await _context.DoctorSchedules
                .AsNoTracking()
                .Where(s => s.DoctorId == doctorId && s.Day == day)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<DoctorSchedule?> GetDoctorScheduleById(string doctorId, int scheduleId, CancellationToken cancellationToken)
        {
            return await _context.DoctorSchedules
                .AsNoTracking()
                .Where(s => s.DoctorId == doctorId && s.Id == scheduleId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<DoctorSchedule?>> GetAllDoctorScheduleById(string doctorId, CancellationToken cancellationToken)
        {
            return await _context.DoctorSchedules.AsNoTracking().Where(s => s.DoctorId == doctorId).ToListAsync(cancellationToken);
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
