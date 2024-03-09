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

        public async Task<List<Message>> GetMessages(string fromUser, string toUser)
        {
            return await _context.Messages.Where(m => m.SenderId == fromUser && m.ReceiverId == toUser).OrderBy(m => m.SendedTime).ToListAsync();
        }
    }
}
