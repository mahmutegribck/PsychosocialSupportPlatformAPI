using PsychosocialSupportPlatformAPI.Business.AppointmentSchedules.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules
{
    public interface IAppointmentScheduleService
    {
        Task AddAppointmentSchedule(AddAppointmentScheduleDTO addAppointmentScheduleDTO);
        Task DeleteAppointmentSchedule(int appointmentScheduleId);
    }
}
