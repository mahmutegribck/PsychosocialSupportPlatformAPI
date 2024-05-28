using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Business.Mails;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.Business.MLModel;
using PsychosocialSupportPlatformAPI.Business.Users.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities.Messages;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;

namespace PsychosocialSupportPlatformAPI.Business.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IMLModelService _mlModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;


        public MessageService(
            IMessageRepository messageRepository,
            IMapper mapper,
            IMLModelService mLModelService,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IMailService mailService
            )
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _mlModelService = mLModelService;
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;

        }
        public async Task AddMessage(SendMessageDto messageDto, string? currentUserId)
        {
            if (currentUserId == null) throw new Exception("Mevcut Kullanıcı Bulunamadı");

            ApplicationUser user = await _userManager.Users.AsNoTracking().Where(u => u.Id == currentUserId).FirstAsync();

            if (await _userManager.IsInRoleAsync(user, _configuration["Roles:Doctor"]))
            {
                messageDto.Emotion = await _mlModelService.GetMessagePrediction(messageDto.Text);
                if (messageDto.Emotion == "Acil Durum")
                {
                    await _mailService.SendEmailToDoctorForEmergency(messageDto.ReceiverId, messageDto.SenderId, messageDto.Text);
                }
            }

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
    }
}
