using Shopper.Core.Components.Entity;

namespace Shopper.Core.Components.Interfaces
{
    public interface IFirebaseEventListener
    {
        event Action<Dictionary<ItemModel, int>>? ItemsUpdated;
        event Action<Dictionary<ItemModel, int>>? SubmittedItemsUpdated;

        public void HandleItems(Dictionary<ItemModel, int> items);

        public void HandleSubmittedItems(Dictionary<ItemModel, int> submittedItems);


    }
}