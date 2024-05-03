namespace PsychosocialSupportPlatformAPI.Business.Messages.DTOs
{
    public class SendMessageDto
    {
        public string Text { get; set; }
        public DateTime SendedTime { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public bool IsSended { get; set; } = false;

    }
}
