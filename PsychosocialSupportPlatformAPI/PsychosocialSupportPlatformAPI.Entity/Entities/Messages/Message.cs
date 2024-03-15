using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Text { get; set; }
        public DateTime SendedTime { get; set; }

        public bool Status { get; set; } = false;

        public bool IsSended { get; set; } = true;

        public string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; }

        public ICollection<MessageOutbox> MessageOutboxes { get; set; }
    }
}
