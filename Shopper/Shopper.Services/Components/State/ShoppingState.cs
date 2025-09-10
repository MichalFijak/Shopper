using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Interfaces;
using Shopper.Services.Components.Dtos;
using Shopper.Services.Components.Mappers;
using System.Diagnostics;


namespace Shopper.Services.Components.State
{
    public class ShoppingState
    {
        private readonly IFirebaseEventListener firebaseEventListener;

        private List<ItemDto> items { get; set; } = new();
        private ItemDto itemToModify { get; set; } = new();
        private string selectedList { get; set; } = "Actual_List";

        public event Action? OnChange;

        public ShoppingState(IFirebaseEventListener firebaseEventListener)
        {
            this.firebaseEventListener = firebaseEventListener;
            firebaseEventListener.ItemsUpdated += HandleItemsUpdated;
        }

        private void HandleItemsUpdated(List<ItemModel> itemsUpdated)
        {
            items = itemsUpdated.Select(i => i.ConvertToDto()).ToList();
            NotifyChange();
        }

        public IReadOnlyList<ItemDto> GetItems() => items;
        public IReadOnlyList<ItemDto> GetItemsInCart() => items.Where(i => i.InCart).ToList();
        public ItemDto GetItemToModify() => itemToModify;
        public string GetSelectedList() => selectedList;

        public void UpdateItems(List<ItemDto> updatedItems)
        {
            items = updatedItems;

            NotifyChange();
            Debug.WriteLine(OnChange?.GetInvocationList().Length ?? 0);
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
