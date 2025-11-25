using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shopper.Core.Components.Configs;
using Shopper.Data;
using Shopper.Services;

namespace Shopper
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            #region Services Registration
            builder.Services.AddServicesDependecies();

            using var stream = FileSystem.OpenAppPackageFileAsync("appsettings.json").Result;
            var config = new ConfigurationBuilder()
                            .AddJsonStream(stream)
                            .Build();

            var projectId = config["Firestore:ProjectId"];
            var credentialFile = config["Firestore:CredentialPath"];

            using var credStream = FileSystem.OpenAppPackageFileAsync(credentialFile).Result;
            var localPath = Path.Combine(FileSystem.CacheDirectory, credentialFile);
            using (var file = File.Create(localPath))
            {
                credStream.CopyTo(file);
            }

            var firestoreOptions = new FirestoreOptions
            {
                ProjectId = projectId,
                CredentialPath = localPath
            };

            builder.Services.AddDataDependencies(firestoreOptions);

            #endregion

            return builder.Build();
        }
    }
}
