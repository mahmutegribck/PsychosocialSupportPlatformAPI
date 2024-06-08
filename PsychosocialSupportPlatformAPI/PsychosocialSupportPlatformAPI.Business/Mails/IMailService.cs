using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.Business.Mails
{
    public interface IMailService
    {
        Task SendEmailToPatientForCancelAppointment(AppointmentSchedule appointment, CancellationToken cancellationToken);
        Task SendEmailToDoctorForCancelAppointment(AppointmentSchedule appointment, CancellationToken cancellationToken);
        Task SendEmailToDoctorForConfirmationAccount(Doctor doctor, CancellationToken cancellationToken);
        Task SendEmailForForgotPassword(ApplicationUser user, string token, CancellationToken cancellationToken);
        Task SendEmailForConfirmEmail(string email, string token, CancellationToken cancellationToken);
        Task SendEmailToDoctorForEmergency(string doctorId, string patientId, string message);
    }
}
