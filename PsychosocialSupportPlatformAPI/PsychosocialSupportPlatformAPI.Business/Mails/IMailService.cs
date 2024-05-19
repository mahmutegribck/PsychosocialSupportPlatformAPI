using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;

namespace PsychosocialSupportPlatformAPI.Business.Mails
{
    public interface IMailService
    {
        Task CancelAppointmentSendEmailToPatient(AppointmentSchedule appointment);
    }
}
