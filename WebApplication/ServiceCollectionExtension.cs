using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace WebApplication;

public static class ServiceCollectionExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
    }
}