namespace PsychosocialSupportPlatformAPI.Business.Mails.DTOs
{
    public class MailDto
    {
        public string[] ToKullanici { get; set; }
        public string Baslik { get; set; } = string.Empty;
        public string Icerik { get; set; } = string.Empty;
    }
}
