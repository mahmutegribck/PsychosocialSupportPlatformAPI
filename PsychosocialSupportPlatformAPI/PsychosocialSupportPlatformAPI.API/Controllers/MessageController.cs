using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Messages;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;
using System.Runtime.CompilerServices;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
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
    }
}
