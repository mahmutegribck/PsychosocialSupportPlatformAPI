using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PsychosocialSupportPlatformAPI.Business.Messages;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.Business.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PsychosocialSupportPlatformAPI.API.Chat
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public static List<string> BagliKullaniciIdler { get; } = new List<string>();

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task SendMessageToUser(string senderID, string receiverID, string message)
        {
            SendMessageDto messageDto = new()
            {
                SendedTime = DateTime.Now,
                ReceiverId = receiverID,
                SenderId = senderID,
                Text = message
            };
            if (BagliKullaniciIdler.Contains(receiverID))
            {
                messageDto.IsSended = true;
                await Clients.Group(receiverID).SendAsync("messageToUserReceived", senderID, receiverID, message);
            }
            await _messageService.AddMessage(messageDto);
        }

        public override async Task OnConnectedAsync()
        {
            var kullaniciId = Context.UserIdentifier;

            if (kullaniciId == null)
                throw new Exception("Kullanıcı bulunamadı.");

            var outboxMessages = await _messageService.GetOutboxMessages(kullaniciId);
            foreach (var outboxMessage in outboxMessages)
            {
                await Clients.Group(outboxMessage.ReceiverId).SendAsync("messageToUserReceived", outboxMessage.SenderId, outboxMessage.ReceiverId, outboxMessage.Text);
                await _messageService.SetSendedMessage(outboxMessage.MessageId);
            }
            if (kullaniciId == null)
                throw new Exception("kullanici adı bulunamadı.");

            lock (BagliKullaniciIdler)
            {
                if (!BagliKullaniciIdler.Contains(kullaniciId))
                    BagliKullaniciIdler.Add(kullaniciId);
            }
            await Groups.AddToGroupAsync(Context.ConnectionId, kullaniciId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var kullaniciId = Context.UserIdentifier;

            if (kullaniciId == null)
            {
                var mesaj = $"kullanici bulunamadı. ex.Message: {exception?.Message}";
                throw new Exception(mesaj);
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, kullaniciId);

            lock (BagliKullaniciIdler)
            {
                BagliKullaniciIdler.Remove(
                    kullaniciId
                );
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}