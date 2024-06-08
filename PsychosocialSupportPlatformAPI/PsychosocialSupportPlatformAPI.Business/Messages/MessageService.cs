using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PsychosocialSupportPlatformAPI.Business.Mails;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.Business.MLModel;
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


        public async Task<List<object>> GetMessagedUsers(string userId, CancellationToken cancellationToken)
        {
            return await _messageRepository.GetMessagedUsers(userId, cancellationToken);
        }


        public async Task<List<GetMessageDto>> GetMessages(GetUserMessageDto getUserMessageDto, CancellationToken cancellationToken)
        {
            try
            {
                if (getUserMessageDto == null) throw new ArgumentNullException("Veriler Eksik");
                return _mapper.Map<List<GetMessageDto>>(await _messageRepository.GetMessages(getUserMessageDto.SenderId, getUserMessageDto.ReceiverId, cancellationToken));

            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<bool> MessageChangeStatus(SetUserMessages setUserMessages, CancellationToken cancellationToken)
        {
            return await _messageRepository.MessageChangeStatus(setUserMessages.SenderId, setUserMessages.ReceiverId, cancellationToken);
        }


        public async Task<GetMessageEmotionDTO> GetPatientAllMessageEmotions(string patientUserName, CancellationToken cancellationToken)
        {
            IEnumerable<string?> messageEmotions = await _messageRepository.GetPatientAllMessageEmotions(patientUserName, cancellationToken);
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
                ScaredMessageRatio = Math.Round((double)scaredMessages / totalMessageEmotions, 2),
                BoredMessageRatio = Math.Round((double)boredMessages / totalMessageEmotions, 2),
                HappyMessageRatio = Math.Round((double)happyMessages / totalMessageEmotions, 2),
                AngryMessageRatio = Math.Round((double)angryMessages / totalMessageEmotions, 2),
                SadMessageRatio = Math.Round((double)sadMessages / totalMessageEmotions, 2),
                EmergencyMessageRatio = Math.Round((double)emergencyMessages / totalMessageEmotions, 2)
            };
        }


        public async Task<GetMessageEmotionDTO> GetPatientLastMonthMessageEmotions(string patientUserName, CancellationToken cancellationToken)
        {
            IEnumerable<string?> messageEmotions = await _messageRepository.GetPatientLastMonthMessageEmotions(patientUserName, cancellationToken);
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
                ScaredMessageRatio = Math.Round((double)scaredMessages / totalMessageEmotions, 2),
                BoredMessageRatio = Math.Round((double)boredMessages / totalMessageEmotions, 2),
                HappyMessageRatio = Math.Round((double)happyMessages / totalMessageEmotions, 2),
                AngryMessageRatio = Math.Round((double)angryMessages / totalMessageEmotions, 2),
                SadMessageRatio = Math.Round((double)sadMessages / totalMessageEmotions, 2),
                EmergencyMessageRatio = Math.Round((double)emergencyMessages / totalMessageEmotions, 2)
            };
        }


        public async Task<GetMessageEmotionDTO> GetPatientLastDayMessageEmotions(string patientUserName, CancellationToken cancellationToken)
        {
            IEnumerable<string?> messageEmotions = await _messageRepository.GetPatientLastDayMessageEmotions(patientUserName, cancellationToken);
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
                ScaredMessageRatio = Math.Round((double)scaredMessages / totalMessageEmotions, 2),
                BoredMessageRatio = Math.Round((double)boredMessages / totalMessageEmotions, 2),
                HappyMessageRatio = Math.Round((double)happyMessages / totalMessageEmotions, 2),
                AngryMessageRatio = Math.Round((double)angryMessages / totalMessageEmotions, 2),
                SadMessageRatio = Math.Round((double)sadMessages / totalMessageEmotions, 2),
                EmergencyMessageRatio = Math.Round((double)emergencyMessages / totalMessageEmotions, 2)
            };
        }
    }
}
