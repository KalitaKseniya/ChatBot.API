
namespace ChatBot.Core.Interfaces
{
    public interface IRepositoryManager
    {
        IChatRepository Chat { get; }
        void Save();
    }
}
