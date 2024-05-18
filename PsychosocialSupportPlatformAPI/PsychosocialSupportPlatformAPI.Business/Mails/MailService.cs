using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using PsychosocialSupportPlatformAPI.Business.Mails.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Mails
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMail(MailDto mailDto)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_configuration["Mail:Username"]));

                foreach (var kullanici in mailDto.ToKullanici)
                {
                    email.To.Add(MailboxAddress.Parse(kullanici));

                }
                email.Subject = mailDto.Baslik;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailDto.Icerik };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(_configuration["Mail:Host"], Convert.ToInt32(_configuration["Mail:Port"]), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_configuration["Mail:Username"], _configuration["Mail:Password"]);
                smtp.Send(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception)
            {
                throw new Exception("Mail gönderilirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
            }
        }
    }
}
