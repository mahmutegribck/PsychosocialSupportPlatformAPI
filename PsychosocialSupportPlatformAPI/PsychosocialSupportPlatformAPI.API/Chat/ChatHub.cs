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
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;

        public static List<string> BagliKullaniciIdler { get; } = new List<string>();

        public ChatHub(IUserService userService, IMessageService messageService)
        {

            _userService = userService;
            _messageService = messageService;

        }
        public async Task SendMessageToUser(string fromUserId, string toUserId, string message)
        {
            SendMessageDto messageDto = new()
            {
                SendedTime = DateTime.UtcNow,
                ReceiverId = fromUserId,
                SenderId = toUserId,
                Text = message
            };
            if (BagliKullaniciIdler.Contains(toUserId))
            {
                messageDto.IsSended = true;
                await Clients.Group(toUserId).SendAsync("messageToUserReceived", message);
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
                await Clients.Group(outboxMessage.ReceiverId).SendAsync("messageToUserReceived", outboxMessage.Text);
                await _messageService.SetSendedMessage(outboxMessage.MessageId);
            }
            //var kullaniciAdi = Context.GetHttpContext()!.User.Identities.FirstOrDefault();

            //var accessToken = Context.GetHttpContext()?.Request.Query["access_token"];
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;

            //// Token içindeki id (user id) bilgisini al
            //var kullaniciId = token?.Claims.First(claim => claim.Type == "nameid").Value;

            if (kullaniciId == null)
                throw new Exception("kullanici adı bulunamadı.");

            //ApplicationUser kullaniciBilgi = (ApplicationUser)await _userService.GetUserByID(userID);
            //if (kullaniciBilgi == null)
            //    throw new Exception("Kullanici bilgisi bulunamadı");


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

            //var accessToken = Context.GetHttpContext()?.Request.Query["access_token"];
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;
            var kullaniciId = Context.UserIdentifier;

            // Token içindeki id (user id) bilgisini al
            //var kullaniciId = token?.Claims.First(claim => claim.Type == "nameid").Value;

            if (kullaniciId == null)
            {
                var mesaj = $"kullanici bulunamadı. ex.Message: {exception?.Message}";
                throw new Exception(mesaj);
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, kullaniciId);
            //var sisKullanici = await _userService.GetUserByID(kullaniciId);

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