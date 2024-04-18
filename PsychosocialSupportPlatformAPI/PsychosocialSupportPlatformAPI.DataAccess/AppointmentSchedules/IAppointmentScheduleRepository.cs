using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules
{
    public interface IAppointmentScheduleRepository
    {
        Task AddAppointmentScheduleList(List<AppointmentSchedule> appointmentSchedules);
        Task AddAppointmentSchedule(AppointmentSchedule appointmentSchedule);
        Task DeleteAppointmentSchedule(AppointmentSchedule appointmentSchedule);







        Task<AppointmentSchedule?> GetAppointmentScheduleByTimeRange(string doctorId, TimeRange timeRange, DateTime day);
        Task<AppointmentSchedule> GetAllAppointmentSchedulesByTimeRange();
        Task<object> GetAllAppointmentSchedulesByDoctor();


    }
}
