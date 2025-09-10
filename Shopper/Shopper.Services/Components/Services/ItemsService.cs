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

    public async Task<List<ItemGroupDto>> GetItemsAsync()
    {
        try
        {
            var models = await firebaseWebhookHandler.GetAllItemsAsync();
            var items = models.Select(m => m.ConvertToDto()).ToList();
            state.UpdateItems(items);
            return cartPolicy.PartitionByCartStatus(items);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching items: {ex.Message}");
            return cartPolicy.PartitionByCartStatus(new List<ItemDto>(state.GetItems()));
        }
    }

    public ItemDto GetItemToModify() => state.GetItemToModify();

    public async Task AddItemAsync(ItemDto item)
    {
        var model = item.ConvertToModel();
        var path = $"ItemList/{model.Name}";
        await firebaseWebhookHandler.CreateItemAsync(path, model);
    }

    public async Task RemoveItemAsync(ItemDto item)
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

    private void OnItemsUpdated(List<ItemModel> updatedItems)
    {
        var updatedDtos = updatedItems.Select(m => m.ConvertToDto()).ToList();
        state.UpdateItems(updatedDtos);
    }

}
