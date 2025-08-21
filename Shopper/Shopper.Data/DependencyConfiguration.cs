using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shopper.Core.Components.Factory;
using Shopper.Core.Components.Interfaces;
using Shopper.Data.Infrastructure.Firebase.Listeners;
using Shopper.Data.Infrastructure.Firebase.Webhooks;

namespace Shopper.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataDependecies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IFirebaseEventListener, FirebaseEventListener>();
            services.AddSingleton<IFirestoreClientFactory, FirestoreClientFactory>(sp =>
            {
                var projectId = configuration["Firestore:ProjectId"];
                var credentialRelativePath = configuration["Firestore:CredentialPath"];
                var basePath = AppContext.BaseDirectory;
                var credentialPath = Path.Combine(basePath, credentialRelativePath);
                if (!File.Exists(credentialPath))
                {
                    throw new FileNotFoundException(
                        "Firebase credentials file not found. Please add credentials.json to the Secrets folder.");
                }

                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);
                var db = FirestoreDb.Create(projectId);
                return new FirestoreClientFactory(db);
            });
            services.AddTransient<IFirebaseWebhookHandler, FirebaseWebhookHandler>();

            return services;
        }
    }
}
