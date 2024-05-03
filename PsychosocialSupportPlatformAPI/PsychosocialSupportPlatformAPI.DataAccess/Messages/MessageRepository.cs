using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;

namespace PsychosocialSupportPlatformAPI.DataAccess.Messages
{
    public class MessageRepository : IMessageRepository
    {
        private readonly PsychosocialSupportPlatformDBContext _context;
        public MessageRepository(PsychosocialSupportPlatformDBContext context)
        {
            _context = context;
        }

        public async Task AddMessage(Message message)
        {
            if (!message.IsSended)
            {
                var outboxMessage = new MessageOutbox
                {
                    SenderId = message.SenderId,
                    ReceiverId = message.ReceiverId,
                    Message = message
                };
                await _context.MessageOutboxes.AddAsync(outboxMessage);
            }
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<object>> GetMessagedUsers(string userId)
        {
            var messagingUsers = await _context.Messages.Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .Select(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .Distinct()
                .Where(id => id != userId)
                .Select(id => new
                {
                    User = _context.Users.Where(u => u.Id == id).Select(u => new
                    {
                        ReceiverId = u.Id,
                        ReceiverName = u.Name,
                        ReceiverSurname = u.Surname,
                        ReceiverProfileImageUrl = u.ProfileImageUrl
                    }).FirstOrDefault(),
                    LastMessage = _context.Messages.Where(msg => (msg.SenderId == userId && msg.ReceiverId == id) || (msg.SenderId == id && msg.ReceiverId == userId))
                    .OrderByDescending(msg => msg.SendedTime)
                    .Select(msg => new
                    {
                        msg.SenderId,
                        msg.Text,
                        msg.SendedTime
                    })
                    .FirstOrDefault(),
                    UnreadMessageCount = _context.Messages.Count(msg => msg.ReceiverId == userId && msg.SenderId == id && !msg.Status)
                })
                .ToListAsync();

            return messagingUsers.Cast<object>().ToList();
        }

        public async Task<List<Message>> GetMessages(string senderId, string receiverId)
        {
            var deneme = await _context.Messages.Include(m => m.Sender).Include(m => m.Receiver).Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == senderId)).OrderBy(m => m.SendedTime).ToListAsync();
            return deneme;
        }
         
        public async Task<List<MessageOutbox>> GetOutboxMessages(string receiverId)
        {
            var outboxMessages = await _context.MessageOutboxes.Where(m => m.ReceiverId == receiverId).ToListAsync();
            return outboxMessages;
        }

        public async Task<bool> MessageChangeStatus(string senderId, string receiverId)
        {
            var userMessage = await _context.Messages.Where(m => m.SenderId == senderId && m.ReceiverId == receiverId && m.Status == false).ToListAsync();

            if (userMessage == null) return false;

            foreach (var user in userMessage)
            {
                user.Status = true;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task SetSendedMessage(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            var messageOutbox = await _context.MessageOutboxes.FindAsync(messageId);
            message.IsSended = true;
            _context.MessageOutboxes.Remove(messageOutbox);
            await _context.SaveChangesAsync();
        }
    }
}
