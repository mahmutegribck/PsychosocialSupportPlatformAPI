using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules
{
    public interface IDoctorScheduleService
    {
        Task CreateDoctorSchedule(List<CreateDoctorScheduleDTO> createDoctorScheduleDTOs, string currentUserID);
        Task UpdateDoctorSchedule(UpdateDoctorScheduleDTO updateDoctorScheduleDTO, string currentUserID);
        Task DeleteDoctorSchedule(string doctorId, int scheduleId);
        Task<GetDoctorScheduleDTO?> GetDoctorScheduleByDay(string doctorId, DateTime day);
        Task<GetDoctorScheduleDTO?> GetDoctorScheduleById(string doctorId, int scheduleId);
        Task<IEnumerable<GetDoctorScheduleDTO?>> GetAllDoctorScheduleById(string doctorId);
        Task<IEnumerable<GetDoctorScheduleDTO?>> GetAllDoctorSchedule();
    }
}
