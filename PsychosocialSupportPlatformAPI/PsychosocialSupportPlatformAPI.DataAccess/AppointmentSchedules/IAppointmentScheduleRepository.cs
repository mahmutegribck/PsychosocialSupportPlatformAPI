using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.DataAccess.AppointmentSchedules
{
    public interface IAppointmentScheduleRepository
    {
        Task AddAppointmentScheduleList(List<AppointmentSchedule> appointmentSchedules);
        Task DeleteAppointmentScheduleList(IEnumerable<AppointmentSchedule> appointmentSchedules);
        Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day);
        Task<IEnumerable<AppointmentSchedule>> GetAppointmentScheduleByDay(string doctorId, DateTime day);
        Task<AppointmentSchedule?> GetAppointmentScheduleByDayAndTimeRange(string doctorId, DateTime day, TimeRange timeRange);

        Task<AppointmentSchedule?> GetAppointmentScheduleById(int appointmentScheduleId);
        Task UpdateAppointmentSchedule(AppointmentSchedule appointmentSchedule);


        Task<IEnumerable<AppointmentSchedule>> AllDoctorAppointments(string doctorId);
        Task<IEnumerable<AppointmentSchedule>> GetAllDoctorAppointmentsByPatientId(string patientId, string doctorId);
        Task<IEnumerable<AppointmentSchedule>> GetAllPastDoctorAppointmentsByPatientSlug(string patientSlug, string doctorId);
        Task<IEnumerable<AppointmentSchedule>> GetAllDoctorAppointmentsByDate(DateTime day, string doctorId);

    }
}
