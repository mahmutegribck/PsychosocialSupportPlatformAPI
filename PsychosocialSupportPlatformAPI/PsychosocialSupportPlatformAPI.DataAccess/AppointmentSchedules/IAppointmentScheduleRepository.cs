using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules
{
    public interface IAppointmentScheduleRepository
    {
        Task AddAppointmentScheduleList(List<AppointmentSchedule> appointmentSchedules);
        Task DeleteAppointmentScheduleList(IEnumerable<AppointmentSchedule> appointmentSchedules);
        Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day);
        Task<IEnumerable<AppointmentSchedule>> GetAppointmentScheduleByDay(string doctorId, DateTime day);


       
        Task<object> GetAllAppointmentSchedulesByDoctor();
    }
}
