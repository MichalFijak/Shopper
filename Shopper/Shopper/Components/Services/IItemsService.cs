using Shopper.Components.Entity;

namespace Shopper.Components.Services
{
    public interface IItemsService
    {
        Dictionary<ItemDto, int> GetShoppingList();
        List<ItemDto> GetSubmittedItems();
        ItemDto GetItemToModify();
        void AddItem(ItemDto item, int quantity);
        void RemoveItem(ItemDto item, int quantity);
        void SubmitItem(ItemDto item);
        void SetItemToModify(ItemDto item);
        void ModifyItemName(ItemDto item, ItemDto newitem, int amount);

    }
}