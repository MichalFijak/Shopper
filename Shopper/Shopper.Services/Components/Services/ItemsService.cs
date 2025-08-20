using Shopper.Services.Components.Dtos;
using Shopper.Services.Components.Services;
using Shopper.Services.Components.State;

public class ItemsService : IItemsService
{
    private readonly ShoppingState state;
    private readonly IFirebaseSyncService firebaseSync;

    public ItemsService(ShoppingState state, IFirebaseSyncService firebaseSync)
    {
        this.state = state;
        this.firebaseSync = firebaseSync;

    }

    public Dictionary<ItemDto, int> GetItems() => new Dictionary<ItemDto, int>(state.GetItems());
    public Dictionary<ItemDto, int> GetItemsInCart() => new Dictionary<ItemDto, int>(state.GetItemsInCart());
    public ItemDto GetItemToModify() => state.GetItemToModify();

    public async Task AddItemAsync(ItemDto item, int quantity)
    {
        await firebaseSync.AddItemAsync(item, quantity);
    }

    public async Task SubmitItemAsync(ItemDto item, int quantity)
    {
        await firebaseSync.SubmitItemAsync(item, quantity);
    }

    public async Task RemoveItemAsync(ItemDto item, int quantity)
    {
        await firebaseSync.RemoveItemAsync(item, quantity);

    }

    public void SetItemToModify(ItemDto item) => state.SetItemToModify(item);



    // list view management
    public string GetSelectedList()
    {
        return state.GetSelectedList();
    }
    public void SetSelectedList(string list)
    {
        state.SetSelectedList(list);
    }
}

