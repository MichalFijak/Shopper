using Microsoft.Extensions.DependencyInjection;

namespace Shopper.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataDependecies(this IServiceCollection services)
        {
            // Register your data-related services here
            // Example: services.AddScoped<IRepository, RepositoryImplementation>();
            return services;
        }
    }
    
        //implementations of repositories, EF Core or SQLite integrations, remote API calls, etc. It references Core
    
}
