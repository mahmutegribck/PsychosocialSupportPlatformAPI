using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Messages.DTOs
{
    public class GetMessageDto
    {
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }

        public string ReceiverName { get; set; }
        public string ReceiverSurname { get; set; }
        public string Text { get; set; }
        public DateTime SendedTime { get; set; }

    }
}
