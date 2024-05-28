using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.Business.Mails
{
    public interface IMailService
    {
        Task SendEmailToPatientForCancelAppointment(AppointmentSchedule appointment);
        Task SendEmailToDoctorForCancelAppointment(AppointmentSchedule appointment);
        Task SendEmailToDoctorForConfirmationAccount(Doctor doctor);
        Task SendEmailForForgotPassword(ApplicationUser user, string token);
        Task SendEmailToDoctorForEmergency(string doctorId, string patientId, string message);

    }
}
