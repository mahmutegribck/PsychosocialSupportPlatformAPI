namespace PsychosocialSupportPlatformAPI.Business.Messages.DTOs
{
    public class GetOutboxMessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int MessageId { get; set; }
        public string Text { get; set; }

    }
}
