using Microsoft.Extensions.Logging;
using Shopper.Components.State;
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
            builder.Services.AddDataDependecies();

            #endregion
            return builder.Build();
        }
    }
}
