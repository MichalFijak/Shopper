using Google.Cloud.Firestore;
using Shopper.Core.Components.Entity;

namespace Shopper.Core.Components.Interfaces
{
    public interface IFirebaseEventListener
    {
        event Action<List<ItemModel>>? ItemsUpdated;

        event Action<ItemModel>? ItemsRemoved;

        void HandleItems(List<ItemModel> items);

        void HandleItemRemoved(ItemModel item);

    }
}