using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules
{
    public interface IDoctorScheduleRepository
    {
        Task CreateDoctorScheduleList(List<DoctorSchedule> doctorSchedules);
        Task CreateDoctorSchedule(DoctorSchedule doctorSchedule);
        Task UpdateDoctorSchedule(DoctorSchedule doctorSchedule);
        Task DeleteDoctorSchedule(DoctorSchedule doctorSchedule);
        Task<DoctorSchedule?> GetDoctorScheduleByDay(string doctorId, DateTime day);
        Task<DoctorSchedule?> GetDoctorScheduleById(string doctorId, int scheduleId);
        Task<DoctorSchedule?> GetDoctorScheduleByTimeRange(string doctorId, TimeRange timeRange, DateTime day);
        Task<IEnumerable<DoctorSchedule?>> GetAllDoctorScheduleById(string doctorId);
        Task<IEnumerable<DoctorSchedule?>> GetAllDoctorSchedule();
    }
}
