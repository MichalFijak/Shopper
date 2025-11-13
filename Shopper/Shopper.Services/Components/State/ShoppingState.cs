using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Interfaces;
using Shopper.Services.Components.Dtos;
using Shopper.Services.Components.Mappers;



namespace Shopper.Services.Components.State
{
    public class ShoppingState
    {
        private readonly IFirebaseEventListener firebaseEventListener;

        private List<ItemDto> items { get; set; } = new();
        private ItemDto itemToModify { get; set; } = new();
        private string selectedList { get; set; } = "MainList";

        public event Action? OnChange;

        public ShoppingState(IFirebaseEventListener firebaseEventListener)
        {
            this.firebaseEventListener = firebaseEventListener;
            firebaseEventListener.ItemsUpdated += HandleItemsUpdated;
            firebaseEventListener.ItemsRemoved += HandleItemRemoved;
        }

        private void HandleItemsUpdated(List<ItemModel> itemsUpdated)
        {

            foreach (var updated in itemsUpdated)
            {
                var dto = updated.ConvertToDto();
                var index = items.FindIndex(i => i.Name == dto.Name);

                if (index >= 0)
                {
                    items[index] = dto; 
                }
                else
                {
                    items.Add(dto); 
                }
            }

            NotifyChange();
        }

        public IReadOnlyList<ItemDto> GetItems()
        {
            return items;
        }
        public IReadOnlyList<ItemDto> GetItemsInCart() => items.Where(i => i.InCart).ToList();
        public ItemDto GetItemToModify() => itemToModify;
        public string GetSelectedList() => selectedList;

        public void UpdateItems(List<ItemDto> updatedItems)
        {
            foreach (var updated in updatedItems)
            {
                var index = items.FindIndex(i => i.Name == updated.Name);
                if (index >= 0)
                {
                    items[index] = updated;
                }
                else
                {
                    items.Add(updated);
                }
            }

            NotifyChange();
        }
        private void HandleItemRemoved(ItemModel item)
        {
            var name = item.Name;
            items.RemoveAll(i => i.Name == name);
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
