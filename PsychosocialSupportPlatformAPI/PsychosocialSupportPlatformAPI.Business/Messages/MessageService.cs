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


        public async Task AddMessage(SendMessageDto messageDto)
        {
            ApplicationUser user = await _userManager.Users.AsNoTracking().Where(u => u.Id == messageDto.SenderId).FirstAsync();

            if (await _userManager.IsInRoleAsync(user, _configuration["Roles:Patient"]))
            {
                messageDto.Emotion = await _mlModelService.GetMessagePrediction(messageDto.Text);
                if (messageDto.Emotion == "acil durum")
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


        public async Task<GetMessageEmotionDTO> GetPatientAllMessageEmotions(string patientUserName)
        {
            IEnumerable<string?> messageEmotions = await _messageRepository.GetPatientAllMessageEmotions(patientUserName);
            if (!messageEmotions.Any()) throw new Exception();

            int totalMessageEmotions = messageEmotions.Count();

            int scaredMessages = messageEmotions.Count(emotion => emotion?.Equals("Korkmuş", StringComparison.OrdinalIgnoreCase) == true);

            int boredMessages = messageEmotions.Count(emotion => emotion?.Equals("Bıkkın", StringComparison.OrdinalIgnoreCase) == true);

            int happyMessages = messageEmotions.Count(emotion => emotion?.Equals("Mutlu", StringComparison.OrdinalIgnoreCase) == true);

            int angryMessages = messageEmotions.Count(emotion => emotion?.Equals("Kızgın", StringComparison.OrdinalIgnoreCase) == true);

            int sadMessages = messageEmotions.Count(emotion => emotion?.Equals("Üzgün", StringComparison.OrdinalIgnoreCase) == true);

            int emergencyMessages = messageEmotions.Count(emotion => emotion?.Equals("Acil Durum", StringComparison.OrdinalIgnoreCase) == true);

            return new()
            {
                ScaredMessageRatio = (double)scaredMessages / totalMessageEmotions,
                BoredMessageRatio = (double)boredMessages / totalMessageEmotions,
                HappyMessageRatio = (double)happyMessages / totalMessageEmotions,
                AngryMessageRatio = (double)angryMessages / totalMessageEmotions,
                SadMessageRatio = (double)sadMessages / totalMessageEmotions,
                EmergencyMessageRatio = (double)emergencyMessages / totalMessageEmotions
            };
        }


        public async Task<GetMessageEmotionDTO> GetPatientLastMonthMessageEmotions(string patientUserName)
        {
            IEnumerable<string?> messageEmotions = await _messageRepository.GetPatientLastMonthMessageEmotions(patientUserName);
            if (!messageEmotions.Any()) throw new Exception();

            int totalMessageEmotions = messageEmotions.Count();

            int scaredMessages = messageEmotions.Count(emotion => emotion?.Equals("Korkmuş", StringComparison.OrdinalIgnoreCase) == true);

            int boredMessages = messageEmotions.Count(emotion => emotion?.Equals("Bıkkın", StringComparison.OrdinalIgnoreCase) == true);

            int happyMessages = messageEmotions.Count(emotion => emotion?.Equals("Mutlu", StringComparison.OrdinalIgnoreCase) == true);

            int angryMessages = messageEmotions.Count(emotion => emotion?.Equals("Kızgın", StringComparison.OrdinalIgnoreCase) == true);

            int sadMessages = messageEmotions.Count(emotion => emotion?.Equals("Üzgün", StringComparison.OrdinalIgnoreCase) == true);

            int emergencyMessages = messageEmotions.Count(emotion => emotion?.Equals("Acil Durum", StringComparison.OrdinalIgnoreCase) == true);

            return new()
            {
                ScaredMessageRatio = (double)scaredMessages / totalMessageEmotions,
                BoredMessageRatio = (double)boredMessages / totalMessageEmotions,
                HappyMessageRatio = (double)happyMessages / totalMessageEmotions,
                AngryMessageRatio = (double)angryMessages / totalMessageEmotions,
                SadMessageRatio = (double)sadMessages / totalMessageEmotions,
                EmergencyMessageRatio = (double)emergencyMessages / totalMessageEmotions
            };
        }


        public async Task<GetMessageEmotionDTO> GetPatientLastDayMessageEmotions(string patientUserName)
        {
            IEnumerable<string?> messageEmotions = await _messageRepository.GetPatientLastDayMessageEmotions(patientUserName);
            if (!messageEmotions.Any()) throw new Exception();

            int totalMessageEmotions = messageEmotions.Count();

            int scaredMessages = messageEmotions.Count(emotion => emotion?.Equals("Korkmuş", StringComparison.OrdinalIgnoreCase) == true);

            int boredMessages = messageEmotions.Count(emotion => emotion?.Equals("Bıkkın", StringComparison.OrdinalIgnoreCase) == true);

            int happyMessages = messageEmotions.Count(emotion => emotion?.Equals("Mutlu", StringComparison.OrdinalIgnoreCase) == true);

            int angryMessages = messageEmotions.Count(emotion => emotion?.Equals("Kızgın", StringComparison.OrdinalIgnoreCase) == true);

            int sadMessages = messageEmotions.Count(emotion => emotion?.Equals("Üzgün", StringComparison.OrdinalIgnoreCase) == true);

            int emergencyMessages = messageEmotions.Count(emotion => emotion?.Equals("Acil Durum", StringComparison.OrdinalIgnoreCase) == true);

            return new()
            {
                ScaredMessageRatio = (double)scaredMessages / totalMessageEmotions,
                BoredMessageRatio = (double)boredMessages / totalMessageEmotions,
                HappyMessageRatio = (double)happyMessages / totalMessageEmotions,
                AngryMessageRatio = (double)angryMessages / totalMessageEmotions,
                SadMessageRatio = (double)sadMessages / totalMessageEmotions,
                EmergencyMessageRatio = (double)emergencyMessages / totalMessageEmotions
            };
        }
    }
}
