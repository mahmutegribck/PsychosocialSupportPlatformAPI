using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules
{
    public interface IDoctorScheduleRepository
    {
        Task CreateDoctorSchedule(DoctorSchedule doctorSchedule);
        Task UpdateDoctorSchedule(DoctorSchedule doctorSchedule);
        Task DeleteDoctorSchedule(int doctorScheduleId);
        Task<DoctorSchedule> GetDoctorSchedule(string doctorId, DoctorSchedule doctorSchedule);
        Task<DoctorSchedule> GetDoctorScheduleById(string doctorId, int scheduleId);

        Task<DoctorSchedule> GetDoctorScheduleByTimeRange(string doctorId, TimeRange timeRange, DayOfWeek day);
        Task<IEnumerable<DoctorSchedule>> GetAllDoctorScheduleById(string doctorId);
        Task<IEnumerable<DoctorSchedule>> GetAllDoctorSchedule();
    }
}
