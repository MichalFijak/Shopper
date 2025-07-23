using Shopper.Components.Entity;

namespace Shopper.Components.Services
{
    public interface IItemsService
    {
        Dictionary<ItemDto, int> GetShoppingList();
        Dictionary<ItemDto, int> GetSubmittedItems();
        ItemDto GetItemToModify();
        void AddItem(ItemDto item, int quantity);
        void RemoveItem(ItemDto item, int quantity);
        void SubmitItem(ItemDto item, int quantity);
        void SetItemToModify(ItemDto item);
        void ModifyItem(ItemDto item, ItemDto newitem, int amount);

    }
}