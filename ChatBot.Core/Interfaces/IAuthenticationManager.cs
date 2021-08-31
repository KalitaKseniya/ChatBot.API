
using ChatBot.Core.Dtos;
using System.Threading.Tasks;

namespace ChatBot.Core.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userDto);
        Task<string> CreateToken();
    }
}
