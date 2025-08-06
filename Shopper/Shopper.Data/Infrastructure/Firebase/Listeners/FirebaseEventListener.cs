using Shopper.Core.Components.Entity;


namespace Shopper.Data.Infrastructure.Firebase.Listeners
{

    public class FirebaseEventListener : IFirebaseEventListener
    {
        public event Action<Dictionary<ItemModel, int>>? ItemsUpdated;
        public event Action<Dictionary<ItemModel, int>>? SubmittedItemsUpdated;


        public void HandleItems(Dictionary<ItemModel, int> items)
        {
            ItemsUpdated?.Invoke(items);
        }

        public void HandleSubmittedItems(Dictionary<ItemModel, int> submittedItems)
        {
            SubmittedItemsUpdated?.Invoke(submittedItems);
        }

    }
}