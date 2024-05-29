using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PsychosocialSupportPlatformAPI.Business.Messages;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;

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
                Text = message,
            };
            if (BagliKullaniciIdler.Contains(receiverID))
            {
                await Clients.Group(receiverID).SendAsync("messageToUserReceived", senderID, receiverID, message);
            }
            await _messageService.AddMessage(messageDto);
        }

        public override async Task OnConnectedAsync()
        {
            var kullaniciId = (Context.User?.Identity?.Name) ?? throw new Exception("Kullanıcı bulunamadı.");

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
            var kullaniciId = (Context.User?.Identity?.Name) 
                ?? throw new Exception($"kullanici bulunamadı. ex.Message: {exception?.Message}");

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