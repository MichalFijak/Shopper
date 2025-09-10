using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Interfaces;


namespace Shopper.Data.Infrastructure.Firebase.Listeners
{

    public class FirebaseEventListener : IFirebaseEventListener
    {
        public event Action<List<ItemModel>>? ItemsUpdated;


        public void HandleItems(List<ItemModel> items)
        {
            ItemsUpdated?.Invoke(items);
        }
    }
}