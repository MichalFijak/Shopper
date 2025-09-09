
using Microsoft.Extensions.Configuration;
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
        public static IServiceCollection AddDataDependecies(this IServiceCollection services)
        {

            var basePath = AppContext.BaseDirectory;

            var config = new ConfigurationBuilder()
                                .SetBasePath(basePath)
                                .AddJsonFile("appsettings.json", optional: true)
                                .Build();


            var projectId = config["Firestore:ProjectId"];
            var credentialPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, config["Firestore:CredentialPath"]));

            if (!File.Exists(credentialPath))
                throw new FileNotFoundException($"Firestore credentials not found at: {credentialPath}");


            services.Configure<FirestoreOptions>(options =>
            {
                options.ProjectId = projectId;
                options.CredentialPath = credentialPath;
            });
            services.AddSingleton<IFirestoreClientFactory, FirestoreClientFactory>(options =>
            {
                return new FirestoreClientFactory(options.GetRequiredService<Microsoft.Extensions.Options.IOptions<FirestoreOptions>>());
            });

            services.AddSingleton<IFirebaseEventListener, FirebaseEventListener>();
            services.AddTransient<IFirebaseWebhookHandler, FirebaseWebhookHandler>();
            services.AddTransient<IFirebaseEventSource, FirebaseEventSource>();

            return services;
        }
    }
}
