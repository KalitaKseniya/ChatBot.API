using ChatBot.Core.Interfaces;
using ChatBot.Infrastructure;
using ChatBot.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureLoggerService(this IServiceCollection services) 
            => services.AddScoped<ILoggerManager, LoggerManager>();
    }
}
