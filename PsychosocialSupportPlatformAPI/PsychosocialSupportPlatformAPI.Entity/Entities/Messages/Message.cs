using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsychosocialSupportPlatformAPI.Entity.Entities
{
    public class Message
    {
        public Message()
        {
            MessageOutboxes = new HashSet<MessageOutbox>();
        }

        [Key]
        public int Id { get; set; }
        public required string Text { get; set; }
        public DateTime SendedTime { get; set; }

        public bool Status { get; set; } = false;

        public bool IsSended { get; set; } = true;

        public required string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; } = null!;

        public required string ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; } = null!;

        public ICollection<MessageOutbox> MessageOutboxes { get; set; }
    }
}
