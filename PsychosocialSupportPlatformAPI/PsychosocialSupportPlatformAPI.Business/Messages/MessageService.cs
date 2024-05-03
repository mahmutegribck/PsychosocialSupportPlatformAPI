using AutoMapper;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities;

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

        public async Task<List<object>> GetMessagedUsers(string userId)
        {
            return await _messageRepository.GetMessagedUsers(userId);
        }

        public async Task<List<GetMessageDto>> GetMessages(GetUserMessageDto getUserMessageDto)
        {
            try
            {
                if (getUserMessageDto == null) throw new ArgumentNullException("Veriler Eksik");
                return _mapper.Map<List<GetMessageDto>>(await _messageRepository.GetMessages(getUserMessageDto.SenderId, getUserMessageDto.ReceiverId));

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> MessageChangeStatus(SetUserMessages setUserMessages)
        {
            return await _messageRepository.MessageChangeStatus(setUserMessages.SenderId, setUserMessages.ReceiverId);
        }

        public async Task<List<GetOutboxMessageDto>> GetOutboxMessages(string receiverId)
        {
            return _mapper.Map<List<GetOutboxMessageDto>>(await _messageRepository.GetOutboxMessages(receiverId));
        }

        public async Task SetSendedMessage(int messageId)
        {
            await _messageRepository.SetSendedMessage(messageId);
        }
    }
}
