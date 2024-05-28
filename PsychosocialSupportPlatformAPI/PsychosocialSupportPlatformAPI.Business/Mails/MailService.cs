using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace PsychosocialSupportPlatformAPI.Business.Mails
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Doctor> _doctorManager;
        private readonly UserManager<Patient> _patientManager;

        public MailService(
            IConfiguration configuration,
            UserManager<Doctor> doctorManager,
            UserManager<Patient> patientManager)
        {
            _configuration = configuration;
            _doctorManager = doctorManager;
            _patientManager = patientManager;
        }

        public async Task SendEmailToDoctorForCancelAppointment(AppointmentSchedule appointment)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Randevunuz İptal Olmuştur",
                IsBodyHtml = true,
                Body = $"{appointment.Day.ToShortDateString()} {(int)appointment.TimeRange}.00 Tarihli {appointment.Patient!.Name} {appointment.Patient.Surname} İle Olan Randevunuz İptal Olmuştur.",
            };

            mail.To.Add(new MailAddress(appointment.Doctor!.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail);
        }

        public async Task SendEmailToPatientForCancelAppointment(AppointmentSchedule appointment)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Randevunuz İptal Olmuştur",
                IsBodyHtml = true,
                Body = $"{appointment.Day.ToShortDateString()} {(int)appointment.TimeRange}.00 Tarihli {appointment.Doctor.DoctorTitle.Title} {appointment.Doctor.Name} {appointment.Doctor.Surname} İle Olan Randevunuz İptal Olmuştur.",
            };

            mail.To.Add(new MailAddress(appointment.Patient!.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail);
        }

        public async Task SendEmailToDoctorForConfirmationAccount(Doctor doctor)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Hesabınız Onaylanmıştır",
                IsBodyHtml = true,
                Body = $"Sayın {doctor.DoctorTitle.Title} {doctor.Name} {doctor.Surname} Hesabınız Onaylanmıştır. Sisteme Erişim Sağlayabilirsiniz.",
            };

            mail.To.Add(new MailAddress(doctor.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail);
        }

        public async Task SendEmailForForgotPassword(ApplicationUser user, string token)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Şifremi Unuttum",
                IsBodyHtml = true,
                Body = $"<h3>Sayın {user.Name} {user.Surname} Şifrenizi Güncellemek İçin Bağlantıya Tıklayın:</h3><br>" +
                $"{_configuration["Urls:DevBaseUrl"]}/api/Authentication/ResetPassword?token={HttpUtility.UrlEncode(token)}?mail={user.Email}",
            };
            mail.To.Add(new MailAddress(user.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail);
        }

        public async Task SendEmailToDoctorForEmergency(string doctorId, string patientId, string message)
        {
            Patient patient = await _patientManager.Users.AsNoTracking().Where(p => p.Id == patientId).FirstAsync();
            Doctor doctor = await _doctorManager.Users.AsNoTracking().Where(d => d.Id == doctorId).FirstAsync();

            var mail = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Acil Durum Bildirimi",
                IsBodyHtml = true,
                Body = $"<h3>Sayın {doctor.DoctorTitle.Title} {doctor.Name} {doctor.Surname} Danışanınız {patient.Name} {patient.Surname} Acil Durum İçeren Mesaj Gönderdi.</h4><br><br>" +
                $"Gönderilen Mesaj: {message}",
            };
            mail.To.Add(new MailAddress(doctor.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(mail);
        }
    }
}
