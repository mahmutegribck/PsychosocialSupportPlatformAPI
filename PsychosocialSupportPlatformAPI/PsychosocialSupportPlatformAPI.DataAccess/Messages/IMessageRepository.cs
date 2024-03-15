using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.DataAccess.Messages
{
    public interface IMessageRepository
    {
        Task AddMessage(Message message);

        Task<List<Message>> GetMessages(string senderId, string receiverId);

        Task<bool> MessageChangeStatus(string senderId, string receiverId);

        Task<List<object>> GetMessagedUsers(string userId);

        Task<List<MessageOutbox>> GetOutboxMessages(string receiverId);

        Task SetSendedMessage(int messageId);
    }
}
