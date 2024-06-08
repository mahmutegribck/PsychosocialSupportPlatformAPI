using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;

namespace PsychosocialSupportPlatformAPI.Business.Messages
{
    public interface IMessageService
    {
        Task AddMessage(SendMessageDto messageDto);
        Task<List<GetMessageDto>> GetMessages(GetUserMessageDto getUserMessageDto, CancellationToken cancellationToken);
        Task<bool> MessageChangeStatus(SetUserMessages setUserMessages, CancellationToken cancellationToken);
        Task<List<object>> GetMessagedUsers(string userId, CancellationToken cancellationToken);
        Task<GetMessageEmotionDTO> GetPatientAllMessageEmotions(string patientUserName, CancellationToken cancellationToken);
        Task<GetMessageEmotionDTO> GetPatientLastMonthMessageEmotions(string patientUserName, CancellationToken cancellationToken);
        Task<GetMessageEmotionDTO> GetPatientLastDayMessageEmotions(string patientUserName, CancellationToken cancellationToken);

    }
}
