using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Messages.DTOs
{
    public class GetMessageDto
    {
        public string Text { get; set; }
        public DateTime SendedTime { get; set; }

    }
}
