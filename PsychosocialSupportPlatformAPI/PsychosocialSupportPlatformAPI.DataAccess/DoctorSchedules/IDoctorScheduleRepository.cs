using PsychosocialSupportPlatformAPI.Entity.Entities;

namespace PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules
{
    public interface IDoctorScheduleRepository
    {
        Task CreateDoctorSchedule(DoctorSchedule doctorSchedule);
        Task UpdateDoctorSchedule(DoctorSchedule doctorSchedule);
        Task DeleteDoctorSchedule(int doctorScheduleId);
        Task<DoctorSchedule> GetDoctorScheduleById(string doctorId, int scheduleId);
        Task<IEnumerable<DoctorSchedule>> GetAllDoctorScheduleById(string doctorId);
    }
}
