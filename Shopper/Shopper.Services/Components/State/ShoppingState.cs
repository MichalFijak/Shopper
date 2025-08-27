using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Interfaces;
using Shopper.Services.Components.Dtos;
using Shopper.Services.Components.Mappers;

namespace Shopper.Services.Components.State
{
    public class ShoppingState
    {

        private readonly IFirebaseEventListener firebaseEventListener;

        private Dictionary<ItemDto, int> items { get; set; } = [];
        private Dictionary<ItemDto, int> itemsInCart { get; set; } = [];
        private ItemDto itemToModify { get; set; } = new();
        private string selectedList { get; set; } = "Actual_List";

        public event Action? OnChange;

        public ShoppingState(IFirebaseEventListener firebaseEventListener)
        {
            this.firebaseEventListener = firebaseEventListener;

            firebaseEventListener.ItemsUpdated += HandleItemsUpdated;
            firebaseEventListener.SubmittedItemsUpdated += HandleSubmittedItemsUpdated;
        }

        private void HandleItemsUpdated(Dictionary<ItemModel, int> itemsUpdated)
        {
            items = itemsUpdated.ConvertToDto();
            NotifyChange();
        }

        private void HandleSubmittedItemsUpdated(Dictionary<ItemModel, int> submittedItems)
        {
            itemsInCart = submittedItems.ConvertToDto();
            NotifyChange();
        }

        public IReadOnlyDictionary<ItemDto, int> GetItems() => items;
        public IReadOnlyDictionary<ItemDto, int> GetItemsInCart() => itemsInCart;
        public ItemDto GetItemToModify() => itemToModify;
        public string GetSelectedList() => selectedList;

        public void UpdateItems(Dictionary<ItemDto, int> updatedItems)
        {
            items = updatedItems;
            NotifyChange();
        }
        public void UpdateItemsInCart(Dictionary<ItemDto, int> updatedItemsInCart)
        {
            itemsInCart = updatedItemsInCart;
            NotifyChange();
        }

        public void SetItemToModify(ItemDto item)
        {
            itemToModify = item;
            NotifyChange();
        }

        public void SetSelectedList(string list)
        {
            selectedList = list;
            NotifyChange();
        }

        private void NotifyChange() => OnChange?.Invoke();
    }
}
