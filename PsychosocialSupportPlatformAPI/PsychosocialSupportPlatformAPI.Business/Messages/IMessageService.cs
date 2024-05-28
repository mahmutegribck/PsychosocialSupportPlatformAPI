using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Messages
{
    public interface IMessageService
    {
        Task AddMessage(SendMessageDto messageDto, string? currentUserId);
        Task<List<GetMessageDto>> GetMessages(GetUserMessageDto getUserMessageDto);
        Task<bool> MessageChangeStatus(SetUserMessages setUserMessages);
        Task<List<object>> GetMessagedUsers(string userId);
        Task<GetMessageEmotionDTO> GetPatientAllMessageEmotions(string patientUserName);
        Task<GetMessageEmotionDTO> GetPatientLastMonthMessageEmotions(string patientUserName);
        Task<GetMessageEmotionDTO> GetPatientLastDayMessageEmotions(string patientUserName);

    }
}
