using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules
{
    public interface IDoctorScheduleService
    {
        Task AddDoctorSchedule(List<CreateDoctorScheduleDTO> createDoctorScheduleDTOs, string currentUserId);
        Task UpdateDoctorSchedule(UpdateDoctorScheduleDTO updateDoctorScheduleDTO, string currentUserId);
        Task DeleteDoctorSchedule(string doctorId, int scheduleId);
        Task<GetDoctorScheduleDTO?> GetDoctorScheduleByDay(string doctorId, DateTime day);
        Task<GetDoctorScheduleDTO?> GetDoctorScheduleById(string doctorId, int scheduleId);
        Task<IEnumerable<GetDoctorScheduleDTO?>> GetAllDoctorScheduleById(string doctorId);
        Task<IEnumerable<object>> GetAllDoctorSchedulesByDate(DateTime day,CancellationToken cancellationToken);
    }
}
