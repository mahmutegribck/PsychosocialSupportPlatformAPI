﻿using PsychosocialSupportPlatformAPI.Entity.Entities;
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

        Task<List<Message>> GetMessages(string fromUser, string toUser);
    }
}