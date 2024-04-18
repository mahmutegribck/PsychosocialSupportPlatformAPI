using PsychosocialSupportPlatformAPI.Business.DoctorSchedules.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.DoctorSchedules
{
    public interface IDoctorScheduleService
    {
        Task CreateDoctorSchedule(CreateDoctorScheduleDTO[] createDoctorScheduleDTOs, string currentUserID);
        Task UpdateDoctorSchedule(UpdateDoctorScheduleDTO updateDoctorScheduleDTO, string currentUserID);
        Task DeleteDoctorSchedule(int doctorScheduleId);
        Task<GetDoctorScheduleDTO> GetDoctorScheduleById(string doctorId, int scheduleId);
        Task<IEnumerable<GetDoctorScheduleDTO>> GetAllDoctorScheduleById(string doctorId);
        Task<IEnumerable<GetDoctorScheduleDTO>> GetAllDoctorSchedule();
    }
}
