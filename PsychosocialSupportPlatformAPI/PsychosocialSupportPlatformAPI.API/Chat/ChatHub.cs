using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PsychosocialSupportPlatformAPI.Business.Messages;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.Business.MLModel;

namespace PsychosocialSupportPlatformAPI.API.Chat
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IMLModelService _mlModelService;

        public static List<string> BagliKullaniciIdler { get; } = new List<string>();

        public ChatHub(
            IMessageService messageService,
            IMLModelService mLModelService
            )
        {
            _messageService = messageService;
            _mlModelService = mLModelService;
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
            await _messageService.AddMessage(messageDto, Context.User?.Identity?.Name);
        }

        public override async Task OnConnectedAsync()
        {
            var kullaniciId = Context.User?.Identity?.Name;

            if (kullaniciId == null)
                throw new Exception("Kullanıcı bulunamadı.");

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
            var kullaniciId = Context.User?.Identity?.Name;

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