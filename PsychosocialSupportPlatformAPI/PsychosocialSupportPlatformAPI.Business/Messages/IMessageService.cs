using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Messages
{
    public interface IMessageService
    {
        Task AddMessage(SendMessageDto messageDto);

        Task<List<GetMessageDto>> GetMessages(GetUserMessageDto getUserMessageDto);
        Task<bool> MessageChangeStatus(SetUserMessages setUserMessages);
    }
}
