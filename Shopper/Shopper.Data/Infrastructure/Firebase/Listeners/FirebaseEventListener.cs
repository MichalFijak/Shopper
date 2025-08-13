using Shopper.Core.Components.Entity;


namespace Shopper.Data.Infrastructure.Firebase.Listeners
{

    public class FirebaseEventListener : IFirebaseEventListener
    {
        public event Action<Dictionary<ItemModelDescription, int>>? ItemsUpdated;
        public event Action<Dictionary<ItemModelDescription, int>>? SubmittedItemsUpdated;


        public void HandleItems(Dictionary<ItemModelDescription, int> items)
        {
            ItemsUpdated?.Invoke(items);
        }

        public void HandleSubmittedItems(Dictionary<ItemModelDescription, int> submittedItems)
        {
            SubmittedItemsUpdated?.Invoke(submittedItems);
        }

    }
}