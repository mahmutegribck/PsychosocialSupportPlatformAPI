using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PsychosocialSupportPlatformAPI.Business.Messages;
using PsychosocialSupportPlatformAPI.Business.Messages.DTOs;

namespace PsychosocialSupportPlatformAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }


        [HttpPost]
        public async Task<IActionResult> GetMessages([FromBody] GetUserMessageDto getUserMessageDto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var messages = await _messageService.GetMessages(getUserMessageDto, cancellationToken);
                if (!messages.Any()) return NotFound();
                return Ok(messages);
            }
            return BadRequest();
        }


        [HttpPatch]
        public async Task<IActionResult> MessageChangeStatus([FromBody] SetUserMessages setUserMessages, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                if (await _messageService.MessageChangeStatus(setUserMessages, cancellationToken)) return Ok();
                return NotFound();
            }
            return BadRequest();
        }


        [HttpGet]
        public async Task<IActionResult> GetMessagedUsers(string userId, CancellationToken cancellationToken)
        {
            var messagedUsers = await _messageService.GetMessagedUsers(userId, cancellationToken);
            if (messagedUsers == null) return NotFound();

            return Ok(messagedUsers);
        }


        [HttpGet]
        [Authorize(Roles = "Doctor, Admin")]
        public async Task<IActionResult> GetPatientAllMessageEmotions(string patientUserName, CancellationToken cancellationToken)
        {
            GetMessageEmotionDTO? messageEmotion = await _messageService.GetPatientAllMessageEmotions(patientUserName, cancellationToken);
            if (messageEmotion == null) return NotFound();

            return Ok(messageEmotion);
        }


        [HttpGet]
        [Authorize(Roles = "Doctor, Admin")]
        public async Task<IActionResult> GetPatientLastMonthMessageEmotions(string patientUserName, CancellationToken cancellationToken)
        {
            GetMessageEmotionDTO? messageEmotion = await _messageService.GetPatientLastMonthMessageEmotions(patientUserName, cancellationToken);
            if (messageEmotion == null) return NotFound();

            return Ok(messageEmotion);
        }


        [HttpGet]
        [Authorize(Roles = "Doctor, Admin")]
        public async Task<IActionResult> GetPatientLastDayMessageEmotions(string patientUserName, CancellationToken cancellationToken)
        {
            GetMessageEmotionDTO? messageEmotion = await _messageService.GetPatientLastDayMessageEmotions(patientUserName, cancellationToken);
            if (messageEmotion == null) return NotFound();

            return Ok(messageEmotion);
        }
    }
}
