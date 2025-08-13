using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.State
{
    public class ShoppingState
    {
        public Dictionary<ItemDto, int> Items { get; private set; } = [];          
        public Dictionary<ItemDto, int> ItemsInCart { get; private set; } = []; 
        public ItemDto ItemToModify { get; private set; } = new();                     
        public string SelectedList { get; private set; } = "Actual_List";             

        public event Action? OnChange;

        
        public void UpdateItems(Dictionary<ItemDto, int> updated)
        {
            Items = updated;
            NotifyChange();
        }

        public void UpdateSubmittedItems(Dictionary<ItemDto, int> updated)
        {
            ItemsInCart = updated;
            NotifyChange();
        }

        public void SetSelectedList(string list)
        {
            SelectedList = list;
            NotifyChange();
        }

        public void SetItemToModify(ItemDto item)
        {
            ItemToModify = item;
            NotifyChange();
        }

        public void NotifyChange() => OnChange?.Invoke();
    }
}
