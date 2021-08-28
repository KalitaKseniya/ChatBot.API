using ChatBot.Core.Interfaces;
using ChatBot.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ChatBot.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryManager(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}
