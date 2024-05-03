using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules
{
    public interface IAppointmentScheduleService
    {
        Task AddAppointmentSchedule(DoctorSchedule doctorSchedule);
        Task UpdateAppointmentSchedule(DoctorSchedule doctorSchedule);
        Task DeleteAppointmentSchedule(string doctorId, DateTime day);
        Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day);
        Task<object> GetAllAppointmentSchedulesByDoctor();

    }
}
