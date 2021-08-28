
using ChatBot.Core.Interfaces;
using ChatBot.Core.Models;

namespace ChatBot.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IChatRepository _chatRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IChatRepository Chat
        {
            get
            {
                if (_chatRepository == null)
                {
                    _chatRepository = new ChatRepository(_repositoryContext);
                }
                return _chatRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
