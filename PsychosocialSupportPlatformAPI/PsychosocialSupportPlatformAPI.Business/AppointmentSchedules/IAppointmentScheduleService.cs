using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs.Doctor;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules
{
    public interface IAppointmentScheduleService
    {
        Task AddAppointmentSchedule(DoctorSchedule doctorSchedule, CancellationToken cancellationToken);
        Task UpdateAppointmentSchedule(DoctorSchedule doctorSchedule, CancellationToken cancellationToken);
        Task DeleteAppointmentSchedule(string doctorId, DateTime day, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day, string patientId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> AllDoctorAppointments(string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllDoctorAppointmentsByPatientId(string patientId, string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<object>> GetAllPastDoctorAppointmentsByPatientSlug(string patientSlug, string doctorId, CancellationToken cancellationToken);
        Task<IEnumerable<GetDoctorAppointmentDTO>> GetAllDoctorAppointmentsByDate(DateTime date, string doctorId, CancellationToken cancellationToken);
        Task<GetDoctorAppointmentDTO> GetDoctorAppointmentByDateAndTimeRange(GetDoctorAppointmentByDateAndTimeRangeDTO getDoctorAppointmentByDateAndTimeRangeDTO, CancellationToken cancellationToken);

    }
}
