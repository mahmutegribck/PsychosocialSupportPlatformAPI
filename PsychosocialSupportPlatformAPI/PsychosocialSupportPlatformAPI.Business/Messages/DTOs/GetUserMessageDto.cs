using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Messages.DTOs
{
    public class GetUserMessageDto
    {
        public string FromUser { get; set; }

        public string ToUser { get; set; }
    }
}
