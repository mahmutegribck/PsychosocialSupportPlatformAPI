using Microsoft.EntityFrameworkCore;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<Message>> GetMessages(string senderId, string receiverId)
        {
            var deneme = await _context.Messages.Include(m => m.Sender).Include(m => m.Receiver).Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == senderId)).OrderBy(m => m.SendedTime).ToListAsync();
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
    }
}
