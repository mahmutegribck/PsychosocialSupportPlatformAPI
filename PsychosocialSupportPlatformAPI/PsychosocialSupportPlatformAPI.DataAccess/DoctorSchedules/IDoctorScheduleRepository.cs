using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.DataAccess.DoctorSchedules
{
    public interface IDoctorScheduleRepository
    {
        Task CreateDoctorScheduleList(List<DoctorSchedule> doctorSchedules, CancellationToken cancellationToken);
        Task CreateDoctorSchedule(DoctorSchedule doctorSchedule, CancellationToken cancellationToken);
        Task UpdateDoctorSchedule(DoctorSchedule doctorSchedule, CancellationToken cancellationToken);
        Task DeleteDoctorSchedule(DoctorSchedule doctorSchedule, CancellationToken cancellationToken);
        Task<DoctorSchedule?> GetDoctorScheduleByDay(string doctorId, DateTime day, CancellationToken cancellationToken);
        Task<DoctorSchedule?> GetDoctorScheduleById(string doctorId, int scheduleId, CancellationToken cancellationToken);
        Task<IEnumerable<DoctorSchedule?>> GetAllDoctorScheduleById(string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<DoctorSchedule?>> GetAllDoctorSchedulesByDate(DateTime day, CancellationToken cancellationToken);
    }
}
