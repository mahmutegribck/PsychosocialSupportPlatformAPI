using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Messages
{
    public class MessageOutbox
    {
        [Key]
        public int Id { get; set; }
        public required string SenderId { get; set; }
        public required string ReceiverId { get; set; }
        public int MessageId { get; set; }
        public Message Message { get; set; } = null!;
    }
}
