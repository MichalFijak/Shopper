using Shopper.Components.Entity;

namespace Shopper.Components.Services
{
    public class ItemsService : IItemsService
    {
        private readonly Dictionary<ItemDto, int> items = [];

        private readonly List<ItemDto> submittedItems = [];

        public Dictionary<ItemDto, int> GetShoppingList()
        {
            return new Dictionary<ItemDto, int>(items);
        }

        public List<ItemDto> GetSubmittedItems()
        {
            return submittedItems;
        }

        public void AddItem(ItemDto item, int quantity)
        {
            if (items.ContainsKey(item))
            {
                items[item] += quantity;
            }
            else
            {
                items[item] = quantity;
            }
        }

        public void RemoveItem(ItemDto item, int quantity)
        {
            if (items.ContainsKey(item))
            {
                items[item] -= quantity;
                if (items[item] <= 0)
                {
                    items.Remove(item);
                }
            }
        }

        public void SubmitItem(ItemDto item)
        {
            submittedItems.Add(item);
        }


    }
}
