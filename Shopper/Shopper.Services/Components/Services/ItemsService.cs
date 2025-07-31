using Shopper.Components.State;
using Shopper.Services.Components.Dtos;

public class ItemsService : IItemsService
{
    private readonly ShoppingState state;

    public ItemsService(ShoppingState state)
    {
        this.state = state;
    }

    public Dictionary<ItemDto, int> GetShoppingList() => state.Items;
    public Dictionary<ItemDto, int> GetSubmittedItems() => state.ItemsInCart;
    public ItemDto GetItemToModify() => state.ItemToModify;

    public void AddItem(ItemDto item, int quantity)
    {
        if (!state.Items.TryAdd(item, quantity))
            state.Items[item] += quantity;

        state.NotifyChange();
    }

    public void RemoveItem(ItemDto item, int quantity)
    {
        if (state.Items.ContainsKey(item))
        {
            state.Items[item] -= quantity;
            if (state.Items[item] <= 0)
                state.Items.Remove(item);

            state.NotifyChange();
        }
    }

    public void SubmitItem(ItemDto item, int quantity)
    {
        state.ItemsInCart[item] = quantity;
        RemoveItem(item, quantity);
    }

    public void ModifyItem(ItemDto item, ItemDto newitem, int amount)
    {
        if (state.Items.TryGetValue(item, out int currentQuantity))
        {
            if (currentQuantity == -amount)
            {
                RemoveItem(item, currentQuantity);
                return;
            }

            state.Items.Remove(item);

            var updatedItem = new ItemDto
            {
                Name = newitem.Name,
                Genre = newitem.Genre,
                Description = newitem.Description,
                Price = newitem.Price
            };

            state.Items[updatedItem] = currentQuantity + amount;
            state.NotifyChange();
        }
    }

    public void SetItemToModify(ItemDto item) => state.SetItemToModify(item);
}

