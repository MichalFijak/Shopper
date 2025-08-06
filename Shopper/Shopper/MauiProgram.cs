using Microsoft.Extensions.Logging;
using Shopper.Components.Heleprs;
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


            var provider = builder.Services.BuildServiceProvider();
            EventWiring.Wire(provider);
            #endregion
            return builder.Build();
        }
    }
}
