using Shopper.Components.Entity;

namespace Shopper.Components.Services
{
    public class ItemsService : IItemsService
    {
        private readonly Dictionary<ItemDto, int> items = [];

        private readonly List<ItemDto> submittedItems = [];

        private ItemDto? itemToModify;
        public Dictionary<ItemDto, int> GetShoppingList()
        {
            return items;
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
            submittedItems.Add(item);

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

        public void ModifyItemName(ItemDto item, ItemDto newitem, int amount)
        {
            if (items.TryGetValue(item, out int quantity))
            {
                if(quantity==-amount)
                {
                    RemoveItem(item, quantity);
                    return;
                }
                items.Remove(item);

                var updatedItem = new ItemDto
                {
                    Name = newitem.Name,
                    Genre = newitem.Genre,
                    Description = newitem.Description,
                    Price = newitem.Price
                };

                items[updatedItem] = quantity +amount;
            }
        }

        public void SetItemToModify(ItemDto item)
        {
            this.itemToModify = item;

        }
        public ItemDto GetItemToModify()
        {
            return itemToModify;
        }
    }
}