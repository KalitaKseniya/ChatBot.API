
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatBot.Core.Dtos
{
    public class UserForCreationDto
    {
        [Required(ErrorMessage = "Email is a required field")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is a required field")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Username is a required field")]
        public string UserName { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
