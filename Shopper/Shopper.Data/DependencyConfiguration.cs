using Microsoft.Extensions.DependencyInjection;
using Shopper.Core.Components.Factory;
using Shopper.Data.Components.Webhooks;
using Shopper.Data.Infrastructure.Firebase.Listeners;
using Shopper.Data.Infrastructure.Firebase.Webhooks;

namespace Shopper.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataDependecies(this IServiceCollection services)
        {
            services.AddSingleton<IFirebaseEventListener, FirebaseEventListener>();
            services.AddSingleton<IFirestoreClientFactory, FirestoreClientFactory>();
            services.AddTransient<IFirebaseWebhookHandler, FirebaseWebhookHandler>();
            // Register your data-related services here
            // Example: services.AddScoped<IRepository, RepositoryImplementation>();
            return services;
        }
    }
    
        //implementations of repositories, EF Core or SQLite integrations, remote API calls, etc. It references Core
    
}
