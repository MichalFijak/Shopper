using Microsoft.Extensions.DependencyInjection;
using Shopper.Services.Components.Policies;
using Shopper.Services.Components.Services;
using Shopper.Services.Components.State;

namespace Shopper.Services
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddServicesDependecies(this IServiceCollection services)
        {

            services.AddScoped<IItemsService, ItemsService>();
            services.AddTransient<ISegregationPolicy, SegregationPolicy>();
            services.AddTransient<IFirebaseSyncService, FirebaseSyncService>();
            services.AddSingleton<ShoppingState>();
            return services;
        }
        //This can sit between App and Data, holding application logic like service classes,
        //orchestrations, use cases, caching, or even platform-specific features. It might reference both Core and Data.
    }
}
