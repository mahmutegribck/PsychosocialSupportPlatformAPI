using PsychosocialSupportPlatformAPI.Business.Appointments.DTOs.Doctor;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.Business.AppointmentSchedules
{
    public interface IAppointmentScheduleService
    {
        Task AddAppointmentSchedule(DoctorSchedule doctorSchedule);
        Task UpdateAppointmentSchedule(DoctorSchedule doctorSchedule);
        Task DeleteAppointmentSchedule(string doctorId, DateTime day);
        Task<IEnumerable<object>> GetAllAppointmentSchedules(DateTime day);

        Task<IEnumerable<object>> AllDoctorAppointments(string doctorId);
        Task<IEnumerable<object>> GetAllDoctorAppointmentsByPatientId(string doctorId, string patientId);
        Task<IEnumerable<object>> GetAllDoctorAppointmentsByDate(DateTime date, string doctorId);



    }
}
