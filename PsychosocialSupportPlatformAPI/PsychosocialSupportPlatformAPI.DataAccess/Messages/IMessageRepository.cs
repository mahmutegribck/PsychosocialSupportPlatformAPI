using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;

namespace PsychosocialSupportPlatformAPI.DataAccess.Messages
{
    public interface IMessageRepository
    {
        Task AddMessage(Message message);
        Task<List<Message>> GetMessages(string senderId, string receiverId);
        Task<bool> MessageChangeStatus(string senderId, string receiverId);
        Task<List<object>> GetMessagedUsers(string userId);
        Task<IEnumerable<string?>> GetPatientAllMessageEmotions(string patientUserName);
        Task<IEnumerable<string?>> GetPatientLastMonthMessageEmotions(string patientUserName);
        Task<IEnumerable<string?>> GetPatientLastDayMessageEmotions(string patientUserName);

    }
}
