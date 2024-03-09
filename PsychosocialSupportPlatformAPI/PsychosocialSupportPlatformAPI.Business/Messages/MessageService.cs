﻿using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychosocialSupportPlatformAPI.Business.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }
        public async Task AddMessage(SendMessageDto messageDto)
        {
            await _messageRepository.AddMessage(_mapper.Map<Message>(messageDto));
        }

        public async Task<List<GetMessageDto>> GetMessages(GetUserMessageDto getUserMessageDto)
        {
            try
            {
                if (getUserMessageDto == null) throw new ArgumentNullException("Veriler Eksik");
                return _mapper.Map<List<GetMessageDto>>(await _messageRepository.GetMessages(getUserMessageDto.FromUser, getUserMessageDto.ToUser));

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
