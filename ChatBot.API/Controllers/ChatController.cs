using ChatBot.Core.Dtos;
using ChatBot.Core.Interfaces;
using ChatBot.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.API.Controllers
{
    [ApiController]
    [Route("api/chats")]
    public class ChatController : Controller
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ChatController(IRepositoryManager repositoryManager,
                              ILoggerManager logger)
        {
            _repository = repositoryManager;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetChats()
        {
            var chats = _repository.Chat.GetAllChats(false);
            return Ok(chats);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PolicyTypes.Chats.ViewById)]
        public IActionResult GetChatById(int id)
        {
            var chat = _repository.Chat.GetChat(id, false);
            if (chat == null)
            {
                _logger.LogWarn($"There is no chat with id = {id}");
                return NotFound();
            }

            return Ok(chat);
        }

        [HttpPost]
        [Authorize(Policy = PolicyTypes.Chats.AddRemove)]
        public IActionResult CreateChat(ChatForManipulationDto chatDto)
        {
            if (chatDto == null)
            {
                _logger.LogWarn("Can't create chatDto == null");
                return BadRequest();
            }
            var chat = new Chat()
            {
                UserRequest = chatDto.UserRequest,
                BotResponse = chatDto.BotResponse,
                NextIds = chatDto.NextIds
            };

            _repository.Chat.CreateChat(chat);
            _repository.Save();

            return Ok(new { id = chat.Id });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = PolicyTypes.Chats.Edit)]
        public IActionResult UpdateChat(int id, ChatForManipulationDto chatDto)
        {
            var chatFromDb = _repository.Chat.GetChat(id, false);
            if (chatFromDb == null || chatDto == null)
            {
                _logger.LogWarn($"Can't update chatDto == null or no chat with id={id}");
                return BadRequest();
            }

            var chat = new Chat()
            {
                Id = id,
                UserRequest = chatDto.UserRequest,
                BotResponse = chatDto.BotResponse,
                NextIds = chatDto.NextIds
            };
            _repository.Chat.UpdateChat(chat);
            _repository.Save();

            return Ok(chat);
        }
        [Authorize(Policy = PolicyTypes.Chats.AddRemove)]
        [HttpDelete("{id}")]
        public IActionResult DeleteChat(int id)
        {
            var chatFromDb = _repository.Chat.GetChat(id, false);
            if (chatFromDb == null)
            {
                _logger.LogWarn($"There is no chat with id={id}");
                return BadRequest();
            }

            _repository.Chat.DeleteChat(chatFromDb);
            _repository.Save();

            return Ok();
        }

    }
}
