
using System.ComponentModel.DataAnnotations;

namespace ChatBot.Core.Dtos
{
    public class ChatForManipulationDto
    {
        [Required(ErrorMessage = "UserRequest is a required field")]
        public string UserRequest { get; set; }
        public string BotResponse { get; set; }
        public string NextIds { get; set; }
    }
}
