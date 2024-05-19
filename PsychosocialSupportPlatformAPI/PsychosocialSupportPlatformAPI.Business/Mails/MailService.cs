using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Entity.Entities.Appointments;
using System.Net;
using System.Net.Mail;

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
                Body = $"{appointment.Day.ToShortDateString()} {appointment.TimeRange}.00 Tarihli {appointment.Patient!.Name} {appointment.Patient.Surname} İle Olan Randevunuz İptal Edilmiştir.",
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
                Body = $"{appointment.Day.ToShortDateString()} {appointment.TimeRange}.00 Tarihli {appointment.Doctor.DoctorTitle.Title} {appointment.Doctor.Name} {appointment.Doctor.Surname} İle Olan Randevunuz İptal Edilmiştir.",
            };
            meesage.To.Add(new MailAddress(appointment.Patient!.Email));

            //foreach (string toUserEmail in mailDto.ToUserEmails)
            //{
            //    meesage.To.Add(new MailAddress(toUserEmail));
            //}

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
