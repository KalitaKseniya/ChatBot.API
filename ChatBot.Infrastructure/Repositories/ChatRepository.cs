using ChatBot.Core.Interfaces;
using ChatBot.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChatBot.Infrastructure.Repositories
{
    public class ChatRepository : RepositoryBase<Chat>, IChatRepository
    {
        public ChatRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Chat> GetAllChats(bool trackChanges)
                => FindAll(trackChanges);

        public void CreateChat(Chat chat) => Create(chat);

        public void DeleteChat(Chat chat) => Delete(chat);

        public Chat GetChat(int Id, bool trackChanges)
            => FindByCondition(ch => ch.Id == Id, trackChanges).FirstOrDefault();

        public void UpdateChat(Chat chat) => Update(chat);
    }
}
