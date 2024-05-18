using PsychosocialSupportPlatformAPI.Business.Mails.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Mails
{
    public interface IMailService
    {
        Task SendMail(MailDto mailDto);
    }
}
