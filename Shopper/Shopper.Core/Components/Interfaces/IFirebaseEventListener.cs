using Shopper.Core.Components.Entity;

namespace Shopper.Core.Components.Interfaces
{
    public interface IFirebaseEventListener
    {
        event Action<List<ItemModel>>? ItemsUpdated;

        void HandleItems(List<ItemModel> items);
    }
}