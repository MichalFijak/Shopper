

using Shopper.Services.Components.Dtos;

public interface IItemsService
{
    public Task AddItemAsync(ItemDto item, int quantity);
    public ItemDto GetItemToModify();
    public Task SubmitItemAsync(ItemDto item, int quantity);

    public Task RemoveItemAsync(ItemDto item, int quantity);

    public void SetItemToModify(ItemDto item);

    public Dictionary<ItemDto, int> GetItems();
    public Dictionary<ItemDto, int> GetItemsInCart();
    public string GetSelectedList();

    public void SetSelectedList(string list);
}

