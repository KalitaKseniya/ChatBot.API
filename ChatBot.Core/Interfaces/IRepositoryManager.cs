
namespace ChatBot.Core.Interfaces
{
    public interface IRepositoryManager
    {
        IChatRepository Chat { get; }
        IPermissionRepository Permission { get; }
        void Save();
    }
}
