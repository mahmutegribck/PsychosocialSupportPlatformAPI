using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PsychosocialSupportPlatformAPI.Entity.Entities.Messages
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public required string Text { get; set; }
        public DateTime SendedTime { get; set; }
        public bool Status { get; set; } = false;
        public required string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; } = null!;

        public required string ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; } = null!;
    }
}
