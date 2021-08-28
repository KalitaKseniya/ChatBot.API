using ChatBot.Core.Dtos;
using ChatBot.Core.Interfaces;
using ChatBot.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace ChatBot.API.Controllers
{
    [ApiController]
    [Route("api/chats")]
    public class ChatController : Controller
    {
        private readonly IRepositoryManager _repository;
        public ChatController(IRepositoryManager repositoryManager)
        {
            _repository = repositoryManager;
        }
        [HttpGet]
        public IActionResult GetChats()
        {
            var chats = _repository.Chat.GetAllChats(false);

            return Ok(chats);
        }

        [HttpGet("{id}")]
        public IActionResult GetChatById(int id)
        {
            var chat = _repository.Chat.GetChat(id, false);
            if(chat == null)
            {
                return NotFound();
            }

            return Ok(chat);
        }

        [HttpPost]
        public IActionResult CreateChat(ChatForManipulationDto chatDto)
        {
            if(chatDto == null)
            {
                return BadRequest();
            }
            var chat = new Chat() { 
                UserRequest =  chatDto.UserRequest,
                BotResponse = chatDto.BotResponse,
                NextIds = chatDto.NextIds
            };
           
            _repository.Chat.CreateChat(chat);
            _repository.Save();

            return Ok(new { id = chat.Id});
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateChat(int id, ChatForManipulationDto chatDto)
        {
            var chatFromDb = _repository.Chat.GetChat(id, false);
            if(chatFromDb == null || chatDto == null)
            {
                return BadRequest();
            }
            
            var chat = new Chat() { 
                Id = id,
                UserRequest =  chatDto.UserRequest,
                BotResponse = chatDto.BotResponse,
                NextIds = chatDto.NextIds
            };
            _repository.Chat.UpdateChat(chat);
            _repository.Save();

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteChat(int id)
        {
            var chatFromDb = _repository.Chat.GetChat(id, false);
            if (chatFromDb == null)
            {
                return BadRequest();
            }
          
            _repository.Chat.DeleteChat(chatFromDb);
            _repository.Save();

            return Ok();
        }

    }
}
