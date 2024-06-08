using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules
{
    public interface IDoctorScheduleService
    {
        Task AddDoctorSchedule(List<CreateDoctorScheduleDTO> createDoctorScheduleDTOs, string currentUserId, CancellationToken cancellationToken);
        Task UpdateDoctorSchedule(UpdateDoctorScheduleDTO updateDoctorScheduleDTO, string currentUserId, CancellationToken cancellationToken);
        Task DeleteDoctorSchedule(string doctorId, int scheduleId, CancellationToken cancellationToken);
        Task<GetDoctorScheduleDTO?> GetDoctorScheduleByDay(string doctorId, DateTime day, CancellationToken cancellationToken);
        Task<GetDoctorScheduleDTO?> GetDoctorScheduleById(string doctorId, int scheduleId, CancellationToken cancellationToken);
        Task<IEnumerable<GetDoctorScheduleDTO?>> GetAllDoctorScheduleById(string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllDoctorSchedulesByDate(DateTime day, CancellationToken cancellationToken);
    }
}
