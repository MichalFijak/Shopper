using Shopper.Services.Components.Dtos;
using Shopper.Services.Components.Mappers;
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

    public async Task<Dictionary<ItemDto, int>> GetItemsAsync()
    {
        Dictionary<ItemDto, int> items = new();

        try
        {
            items =await firebaseSync.GetAllItemsAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching items: {ex.Message}");
        }
 
        if (items != null && items.Count > 0)
        {
           state.UpdateItems(items);
           return items;
        }
        else
        {
            return new Dictionary<ItemDto, int>(state.GetItems());
        }
    
     

    }

    public Dictionary<ItemDto, int> GetItemsInCart()
    {
        // here we need logicPolicy

        throw new NotImplementedException();
    }
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

    public string GetSelectedList()
    {
        return state.GetSelectedList();
    }
    public void SetSelectedList(string list)
    {
        state.SetSelectedList(list);
    }

}

