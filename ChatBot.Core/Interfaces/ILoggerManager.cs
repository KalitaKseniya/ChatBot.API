
namespace ChatBot.Core.Interfaces
{
    public interface ILoggerManager
    {
        void LogError(string message);
        void LogInfo(string message);
        void LogDebug(string message);
        void LogWarn(string message);
    }
}
