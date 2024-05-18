using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Business.Mails.DTOs;
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


        public async Task SendEmail(MailDto mailDto)
        {
            var meesage = new MailMessage()
            {
                From = new MailAddress(_configuration["Mailing:Sender"]!),
                Subject = mailDto.Subject,
                IsBodyHtml = true,
                Body = mailDto.Body
            };

            foreach (string toUserEmail in mailDto.ToUserEmails)
            {
                meesage.To.Add(new MailAddress(toUserEmail));
            }

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
