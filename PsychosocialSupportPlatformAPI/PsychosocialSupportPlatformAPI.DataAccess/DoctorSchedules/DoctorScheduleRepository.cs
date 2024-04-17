using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;

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
            return await _context.DoctorSchedules.Where(s => s.DoctorId == doctorId).ToListAsync();
        }

        public async Task<DoctorSchedule> GetDoctorScheduleById(string doctorId, int scheduleId)
        {
            return await _context.DoctorSchedules.Where(s => s.DoctorId == doctorId && s.Id == scheduleId).FirstOrDefaultAsync();
        }

        public async Task UpdateDoctorSchedule(DoctorSchedule doctorSchedule)
        {
            var updateDoctorSchedule = await _context.DoctorSchedules.Where(s => s.Id == doctorSchedule.Id && s.DoctorId == doctorSchedule.DoctorId).FirstOrDefaultAsync() ?? throw new Exception();
            _context.DoctorSchedules.Update(doctorSchedule);
            await _context.SaveChangesAsync();
        }
    }
}
