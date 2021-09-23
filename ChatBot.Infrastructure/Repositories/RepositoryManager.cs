
using ChatBot.Core.Interfaces;

namespace ChatBot.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IChatRepository _chatRepository;
        private IPermissionRepository _permissionRepository;
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
        public IPermissionRepository Permission
        {
            get
            {
                if (_permissionRepository == null)
                {
                    _permissionRepository = new PermissionRepository(_repositoryContext);
                }
                return _permissionRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
