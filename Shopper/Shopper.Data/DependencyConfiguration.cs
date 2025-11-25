using Microsoft.Extensions.DependencyInjection;
using Shopper.Core.Components.Configs;
using Shopper.Core.Components.Factory;
using Shopper.Core.Components.Interfaces;
using Shopper.Data.Components.Webhooks;
using Shopper.Data.Infrastructure.Firebase.Listeners;
using Shopper.Data.Infrastructure.Firebase.Webhooks;

namespace Shopper.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataDependencies(this IServiceCollection services, FirestoreOptions firestoreOptions)
        {
            // Rejestrujemy opcje przekazane z projektu MAUI
            services.Configure<FirestoreOptions>(options =>
            {
                options.ProjectId = firestoreOptions.ProjectId;
                options.CredentialPath = firestoreOptions.CredentialPath;
            });

            services.AddSingleton<IFirestoreClientFactory, FirestoreClientFactory>(sp =>
            {
                return new FirestoreClientFactory(sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<FirestoreOptions>>());
            });

            services.AddSingleton<IFirebaseEventListener, FirebaseEventListener>();
            services.AddTransient<IFirebaseWebhookHandler, FirebaseWebhookHandler>();
            services.AddTransient<IFirebaseEventSource, FirebaseEventSource>();

            return services;
        }
    }
}
