using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs.Doctor;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Enums;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules
{
    public interface IAppointmentScheduleService
    {
        Task AddAppointmentSchedule(DoctorSchedule doctorSchedule);
        Task UpdateAppointmentSchedule(DoctorSchedule doctorSchedule);
        Task DeleteAppointmentSchedule(string doctorId, DateTime day);
        Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day, string patientId);
        Task<IEnumerable<object>> AllDoctorAppointments(string doctorId);
        Task<IEnumerable<object>> GetAllDoctorAppointmentsByPatientId(string patientId, string doctorId);
        Task<IEnumerable<object>> GetAllPastDoctorAppointmentsByPatientSlug(string patientSlug, string doctorId);
        Task<IEnumerable<GetDoctorAppointmentDTO>> GetAllDoctorAppointmentsByDate(DateTime date, string doctorId);
        Task<GetDoctorAppointmentDTO> GetDoctorAppointmentByDateAndTimeRange(DateTime day, TimeRange timeRange, string doctorId, CancellationToken cancellationToken);

    }
}
