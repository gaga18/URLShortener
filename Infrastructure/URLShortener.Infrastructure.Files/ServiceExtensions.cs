using Project.Core.Application.Interfaces.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Infrastructure.Files
{
    public static class ServiceExtensions
    {
        public static void AddFilesLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileManager, FileManager>();
        }
    }
}
