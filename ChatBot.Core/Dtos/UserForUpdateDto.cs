using System.ComponentModel.DataAnnotations;

namespace ChatBot.Core.Dtos
{
    public class UserForUpdateDto
    {
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is a required field")]
        public string UserName { get; set; }
    }
}
