namespace PsychosocialSupportPlatformAPI.Business.Messages.DTOs
{
    public class SendMessageDto
    {
        public required string Text { get; set; }
        public DateTime SendedTime { get; set; }
        public required string SenderId { get; set; }
        public required string ReceiverId { get; set; }
        public string? Emotion { get; set; }

    }
}
