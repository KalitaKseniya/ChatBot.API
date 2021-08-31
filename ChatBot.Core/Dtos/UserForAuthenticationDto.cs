using System.ComponentModel.DataAnnotations;

namespace ChatBot.Core.Dtos
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Username is a requred field")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is a requred field")]
        public string Password { get; set; }
    }
}
