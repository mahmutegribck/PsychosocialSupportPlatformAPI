using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using PsychosocialSupportPlatformAPI.Business.Messages;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using PsychosocialSupportPlatformAPI.DataAccess;
using PsychosocialSupportPlatformAPI.Entity.Entities;
using PsychosocialSupportPlatformAPI.Entity.Entities.Users;
using System.Runtime.CompilerServices;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly PsychosocialSupportPlatformDBContext _context;
        public MessageController(IMessageService messageService, PsychosocialSupportPlatformDBContext context)
        {
            _messageService = messageService;
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> GetMessages([FromBody] GetUserMessageDto getUserMessageDto)
        {
            if (ModelState.IsValid)
            {
                var messages = await _messageService.GetMessages(getUserMessageDto);
                if (messages == null) return NotFound();
                return Ok(messages);
            }
            return BadRequest();
        }

        [HttpPatch]
        public async Task<IActionResult> MessageChangeStatus([FromBody] SetUserMessages setUserMessages)
        {
            if (ModelState.IsValid)
            {
                if (await _messageService.MessageChangeStatus(setUserMessages)) return Ok();
                return NotFound();
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagedUsers(string userId)
        {
            var messagedUsers = await _messageService.GetMessagedUsers(userId);
            if (messagedUsers == null) return NotFound();

            return Ok(messagedUsers);
        }
    }
}
