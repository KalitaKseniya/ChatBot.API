using ChatBot.Core.Interfaces;
using ChatBot.Core.Models;

namespace ChatBot.Infrastructure.Repositories
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        public ChatRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
