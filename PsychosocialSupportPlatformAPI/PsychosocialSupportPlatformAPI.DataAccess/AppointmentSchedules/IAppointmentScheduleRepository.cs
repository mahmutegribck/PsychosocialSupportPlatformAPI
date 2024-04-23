using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules
{
    public interface IAppointmentScheduleRepository
    {
        Task AddAppointmentScheduleList(List<AppointmentSchedule> appointmentSchedules);
        Task AddAppointmentSchedule(AppointmentSchedule appointmentSchedule);
        Task DeleteAppointmentSchedule(AppointmentSchedule appointmentSchedule);
        Task UpdateAppointmentSchedule(AppointmentSchedule appointmentSchedule);
        Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day);









        Task<AppointmentSchedule?> GetAppointmentScheduleByTimeRange(string doctorId, TimeRange timeRange, DateTime day);
        Task<AppointmentSchedule> GetAllAppointmentSchedulesByTimeRange();
        Task<object> GetAllAppointmentSchedulesByDoctor();


    }
}
