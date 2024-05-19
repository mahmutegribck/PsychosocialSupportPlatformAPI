using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.Business.Mails
{
    public interface IMailService
    {
        Task SendEmailToPatientForCancelAppointment(AppointmentSchedule appointment);
        Task SendEmailToDoctorForCancelAppointment(AppointmentSchedule appointment);
    }
}
