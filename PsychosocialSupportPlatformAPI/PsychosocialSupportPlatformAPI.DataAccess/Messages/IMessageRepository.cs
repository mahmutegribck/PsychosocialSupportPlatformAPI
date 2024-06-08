using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;

namespace PsychosocialSupportPlatformAPI.DataAccess.Messages
{
    public interface IMessageRepository
    {
        Task AddMessage(Message message);
        Task<List<Message>> GetMessages(string senderId, string receiverId, CancellationToken cancellationToken);
        Task<bool> MessageChangeStatus(string senderId, string receiverId, CancellationToken cancellationToken);
        Task<List<object>> GetMessagedUsers(string userId, CancellationToken cancellationToken);
        Task<IEnumerable<string?>> GetPatientAllMessageEmotions(string patientUserName, CancellationToken cancellationToken);
        Task<IEnumerable<string?>> GetPatientLastMonthMessageEmotions(string patientUserName, CancellationToken cancellationToken);
        Task<IEnumerable<string?>> GetPatientLastDayMessageEmotions(string patientUserName, CancellationToken cancellationToken);

    }
}
