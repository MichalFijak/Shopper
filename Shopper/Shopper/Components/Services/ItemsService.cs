using Shopper.Components.Entity;

namespace Shopper.Components.Services
{
    public class ItemsService : IItemsService
    {
        private readonly Dictionary<ItemDto, int> items = [];

        private readonly Dictionary<ItemDto, int> submittedItems = [];

        private ItemDto itemToModify = new();
        public Dictionary<ItemDto, int> GetShoppingList()
        {
            return items;
        }


        //  this method will return list of items that has been submitted (added to the cart)
        public Dictionary<ItemDto, int> GetSubmittedItems()
        {
            return submittedItems;
        }

        public void AddItem(ItemDto item, int quantity)
        {
            if (!items.TryAdd(item, quantity))
            {
                items[item] += quantity;
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

        public void SubmitItem(ItemDto item, int quantity)
        {
            submittedItems.Add(item, quantity);
            RemoveItem(item, quantity);
        }

        public void ModifyItem(ItemDto item, ItemDto newitem, int amount)
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