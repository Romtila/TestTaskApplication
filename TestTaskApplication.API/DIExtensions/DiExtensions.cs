using System.Reflection;
using Mapster;
using MapsterMapper;
using TestTaskApplication.Application.IServices;
using TestTaskApplication.Application.Services;
using TestTaskApplication.Core.Interfaces;
using TestTaskApplication.Infrastructure.Repositories;

namespace TestTaskApplication.API.DIExtensions;

public static class DiExtension
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<INodeService, NodeService>();
        services.AddScoped<IJournalService, JournalService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<INodeRepository, NodeRepository>();
        services.AddScoped<IJournalRepository, JournalRepository>();
    }
    
    public static void AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}