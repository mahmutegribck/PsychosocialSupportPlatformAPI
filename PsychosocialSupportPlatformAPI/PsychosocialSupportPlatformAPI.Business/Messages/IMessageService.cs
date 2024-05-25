using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Messages
{
    public interface IMessageService
    {
        Task AddMessage(SendMessageDto messageDto);
        Task<List<GetMessageDto>> GetMessages(GetUserMessageDto getUserMessageDto);
        Task<bool> MessageChangeStatus(SetUserMessages setUserMessages);
        Task<List<object>> GetMessagedUsers(string userId);
    }
}
