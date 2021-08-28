
using ChatBot.Core.Models;
using System.Collections.Generic;

namespace ChatBot.Core.Interfaces
{
    public interface IChatRepository
    {
        IEnumerable<Chat> GetAllChats(bool trackChanges);
        Chat GetChat(int Id, bool trackChanges);
        void CreateChat(Chat chat);
        void UpdateChat(Chat chat);
        void DeleteChat(Chat chat);
    }
}
