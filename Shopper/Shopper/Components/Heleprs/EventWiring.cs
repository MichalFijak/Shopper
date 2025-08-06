
using Shopper.Components.State;
using Shopper.Data.Components.Webhooks;
using Shopper.Data.Infrastructure.Firebase.Listeners;
using Shopper.Services.Components.Mappers;

namespace Shopper.Components.Heleprs
{
    public static class EventWiring
    {
        public static void Wire(IServiceProvider services)
        {
            var listener = services.GetRequiredService<IFirebaseEventListener>();
            var shoppingState = services.GetRequiredService<ShoppingState>();
        
            listener.ItemsUpdated += items =>
            {
                var mappedItems = items.ToDictionary(kvp=>kvp.Key.ToDto(), kvp=>kvp.Value);
                shoppingState.UpdateItems(mappedItems);
            };

            listener.SubmittedItemsUpdated += submittedItems =>
            {
                var mappedSubmittedItems = submittedItems.ToDictionary(kvp=>kvp.Key.ToDto(), kvp=>kvp.Value);
                shoppingState.UpdateSubmittedItems(mappedSubmittedItems);
            };

        }


    }
}
