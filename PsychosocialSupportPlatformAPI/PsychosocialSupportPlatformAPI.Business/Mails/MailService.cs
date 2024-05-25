using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.Net;
using System.Net.Mail;
using System.Numerics;
using System.Web;

namespace PsychosocialSupportPlatformAPI.Business.Mails
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailToDoctorForCancelAppointment(AppointmentSchedule appointment)
        {
            var meesage = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Randevunuz İptal Olmuştur",
                IsBodyHtml = true,
                Body = $"{appointment.Day.ToShortDateString()} {(int)appointment.TimeRange}.00 Tarihli {appointment.Patient!.Name} {appointment.Patient.Surname} İle Olan Randevunuz İptal Olmuştur.",
            };

            meesage.To.Add(new MailAddress(appointment.Doctor!.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(meesage);
        }


        public async Task SendEmailToPatientForCancelAppointment(AppointmentSchedule appointment)
        {
            var meesage = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Randevunuz İptal Olmuştur",
                IsBodyHtml = true,
                Body = $"{appointment.Day.ToShortDateString()} {(int)appointment.TimeRange}.00 Tarihli {appointment.Doctor.DoctorTitle.Title} {appointment.Doctor.Name} {appointment.Doctor.Surname} İle Olan Randevunuz İptal Olmuştur.",
            };

            meesage.To.Add(new MailAddress(appointment.Patient!.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(meesage);
        }


        public async Task SendEmailToDoctorForConfirmationAccount(Doctor doctor)
        {
            var meesage = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Hesabınız Onaylanmıştır",
                IsBodyHtml = true,
                Body = $"Sayın {doctor.DoctorTitle.Title} {doctor.Name} {doctor.Surname} Hesabınız Onaylanmıştır. Sisteme Erişim Sağlayabilirsiniz.",
            };

            meesage.To.Add(new MailAddress(doctor.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(meesage);
        }

        public async Task SendEmailForForgotPassword(ApplicationUser user, string token)
        {
            var meesage = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = "Şifremi Unuttum",
                IsBodyHtml = true,
                Body = $"<h3>Sayın {user.Name} {user.Surname} Şifrenizi Güncellemek İçin Bağlantıya Tıklayın:</h3><br>" +
                $"{_configuration["Urls:DevBaseUrl"]}/api/Authentication/ResetPassword?token={HttpUtility.UrlEncode(token)}",
            };
            meesage.To.Add(new MailAddress(user.Email));

            using SmtpClient client = new();
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Mailing:Sender"], _configuration["Mailing:Password"]);
            client.Host = _configuration["Mailing:Host"]!;
            client.Port = Convert.ToInt32(_configuration["Mailing:Port"]);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            await client.SendMailAsync(meesage);
        }
    }
}
