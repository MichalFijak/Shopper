using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Interfaces;


namespace Shopper.Data.Infrastructure.Firebase.Listeners
{

    public class FirebaseEventListener : IFirebaseEventListener
    {
        public event Action<List<ItemModel>>? ItemsUpdated;
        public event Action<ItemModel>? ItemsRemoved;
        public void HandleItems(List<ItemModel> items)
        {
            ItemsUpdated?.Invoke(items);
        }
        public void HandleItemRemoved(ItemModel item)
        {
            ItemsRemoved?.Invoke(item);
        }

    }
}
