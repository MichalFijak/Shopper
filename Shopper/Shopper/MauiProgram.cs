using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Shopper.Data;
using Shopper.Services;

namespace Shopper
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var basePath = AppContext.BaseDirectory;
            var configPath = Path.Combine(basePath, "appsettings.json");
            builder.Configuration.AddJsonFile(configPath, optional: false, reloadOnChange: true);

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
            builder.Services.AddDataDependecies(builder.Configuration);


            var provider = builder.Services.BuildServiceProvider();
            #endregion
            return builder.Build();
        }
    }
}


