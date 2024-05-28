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
        public async Task<IActionResult> GetMessages([FromBody] GetUserMessageDto getUserMessageDto)
        {
            if (ModelState.IsValid)
            {
                var messages = await _messageService.GetMessages(getUserMessageDto);
                if (!messages.Any()) return NotFound();
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


        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetPatientAllMessageEmotions(string patientUserName)
        {
            GetMessageEmotionDTO? messageEmotion = await _messageService.GetPatientAllMessageEmotions(patientUserName);
            if (messageEmotion == null) return NotFound();

            return Ok(messageEmotion);
        }


        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetPatientLastMonthMessageEmotions(string patientUserName)
        {
            GetMessageEmotionDTO? messageEmotion = await _messageService.GetPatientLastMonthMessageEmotions(patientUserName);
            if (messageEmotion == null) return NotFound();

            return Ok(messageEmotion);
        }


        [HttpGet]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetPatientLastDayMessageEmotions(string patientUserName)
        {
            GetMessageEmotionDTO? messageEmotion = await _messageService.GetPatientLastDayMessageEmotions(patientUserName);
            if (messageEmotion == null) return NotFound();

            return Ok(messageEmotion);
        }
    }
}
