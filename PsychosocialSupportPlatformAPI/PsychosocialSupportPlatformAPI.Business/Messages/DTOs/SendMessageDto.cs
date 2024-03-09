using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Messages.DTOs
{
    public class SendMessageDto
    {
        public string Text { get; set; }
        public DateTime SendedTime { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }


    }
}
