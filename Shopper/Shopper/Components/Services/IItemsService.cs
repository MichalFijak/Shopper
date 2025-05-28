using Shopper.Components.Entity;

namespace Shopper.Components.Services
{
    public interface IItemsService
    {
        void AddItem(ItemDto item, int quantity);
        Dictionary<ItemDto, int> GetShoppingList();
        List<ItemDto> GetSubmittedItems();
        void RemoveItem(ItemDto item, int quantity);
        void SubmitItem(ItemDto item);
    }
}