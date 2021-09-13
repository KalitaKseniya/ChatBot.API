
using Microsoft.AspNetCore.Identity;

namespace ChatBot.Infrastructure.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
