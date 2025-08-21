using Google.Cloud.Firestore;
using Microsoft.Extensions.DependencyInjection;
using Shopper.Core.Components.Factory;
using Shopper.Core.Components.Interfaces;
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
            services.AddSingleton<IFirestoreClientFactory, FirestoreClientFactory>( sp=>
            {
                var projectId = "shopper-bf898";
                var credentialPath = "path/to/your/credentials.json";

                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);
                var db = FirestoreDb.Create(projectId);
                return new FirestoreClientFactory(db);
            });
            services.AddTransient<IFirebaseWebhookHandler, FirebaseWebhookHandler>();

            return services;
        }
    }
    
    
}
