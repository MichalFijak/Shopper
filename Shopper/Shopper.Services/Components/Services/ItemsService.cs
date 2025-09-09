using Shopper.Core.Components.Dtos;
using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Interfaces;
using Shopper.Data.Components.Webhooks;
using Shopper.Services.Components.Dtos;
using Shopper.Services.Components.Mappers;
using Shopper.Services.Components.Policies;
using Shopper.Services.Components.State;

public class ItemsService : IItemsService
{
    private readonly ShoppingState state;
    private readonly IFirebaseWebhookHandler firebaseWebhookHandler;
    private readonly ICartPolicy cartPolicy;
    private readonly IFirebaseEventListener firebaseListener;
    private readonly IFirebaseEventSource firebaseEventSource;

    public ItemsService(
        ShoppingState state,
        IFirebaseWebhookHandler firebaseWebhookHandler,
        ICartPolicy cartPolicy,
        IFirebaseEventListener firebaseListener,
        IFirebaseEventSource firebaseEventSource)
    {
        this.state = state;
        this.firebaseWebhookHandler = firebaseWebhookHandler;
        this.cartPolicy = cartPolicy;
        this.firebaseListener = firebaseListener;
        this.firebaseEventSource = firebaseEventSource;
    }

    public async Task<Dictionary<ItemDto, int>> GetItemsAsync()
    {
        Dictionary<ItemDto, int> items = new();

        try
        {
            var models = await firebaseWebhookHandler.GetAllItemsAsync();
            foreach (var model in models)
            {
                var dto = model.ConvertToDto();
                items[dto] = 1; // Default quantity, adjust if needed
            }
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

    public async Task<List<ItemGroupDto>> GetItems()
    {
        var items = await GetItemsAsync();
        return cartPolicy.PartitionByCartStatus(items);
    }

    public ItemDto GetItemToModify() => state.GetItemToModify();

    public async Task AddItemAsync(ItemDto item, int quantity)
    {
        var model = item.ConvertToModel();
        var path = $"ItemList/{model.Name}"; // Example path, adjust as needed
        await firebaseWebhookHandler.CreateItemAsync(path, model);
    }

    public async Task SubmitItemAsync(ItemDto item, int quantity)
    {
        var model = item.ConvertToModel();
        var path = $"ItemList/{model.Name}";
        model.InCart = !model.InCart;
        await firebaseWebhookHandler.UpdateItemAsync(path, model);
    }

    public async Task RemoveItemAsync(ItemDto item, int quantity)
    {
        var model = item.ConvertToModel();
        var path = $"ItemList/{model.Name}";
        await firebaseWebhookHandler.DeleteItemAsync(path);
    }

    public void SetItemToModify(ItemDto item) => state.SetItemToModify(item);

    public string GetSelectedList() => state.GetSelectedList();

    public void SetSelectedList(string list) => state.SetSelectedList(list);

    public async Task StartRealtimeSyncAsync()
    {
        firebaseListener.ItemsUpdated += OnItemsUpdated;
        await firebaseEventSource.ListenToEventAsync("");
    }

    public void StopRealtimeSync()
    {
        firebaseListener.ItemsUpdated -= OnItemsUpdated;
    }

    private void OnItemsUpdated(Dictionary<ItemModel, int> updatedItems)
    {
        var updatedDtos = updatedItems.ToDictionary(
            kvp => kvp.Key.ConvertToDto(),
            kvp => kvp.Value);

        state.UpdateItems(updatedDtos);
    }
}
