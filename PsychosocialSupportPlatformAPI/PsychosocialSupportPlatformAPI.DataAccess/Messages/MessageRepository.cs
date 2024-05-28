using Microsoft.EntityFrameworkCore;
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
            var deneme = await _context.Messages.Include(m => m.Sender).Include(m => m.Receiver).Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == senderId)).OrderBy(m => m.SendedTime).AsNoTracking().ToListAsync();
            return deneme;
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


        public async Task<IEnumerable<string?>> GetPatientAllMessageEmotions(string patientUserName)
        {
            return await _context.Messages
                .AsNoTracking()
                .Include(m => m.Sender)
                .Where(m => m.Sender.UserName == patientUserName)
                .Select(m => m.Emotion)
                .ToListAsync();
        }

        public async Task<IEnumerable<string?>> GetPatientLastMonthMessageEmotions(string patientUserName)
        {
            return await _context.Messages
                .AsNoTracking()
                .Include(m => m.Sender)
                .Where(m => m.Sender.UserName == patientUserName && m.SendedTime.Month == DateTime.Now.Month)
                .Select(m => m.Emotion)
                .ToListAsync();
        }


        public async Task<IEnumerable<string?>> GetPatientLastDayMessageEmotions(string patientUserName)
        {
            return await _context.Messages
                .AsNoTracking()
                .Include(m => m.Sender)
                .Where(m => m.Sender.UserName == patientUserName && m.SendedTime.DayOfYear == DateTime.Now.DayOfYear)
                .Select(m => m.Emotion)
                .ToListAsync();
        }
    }
}
