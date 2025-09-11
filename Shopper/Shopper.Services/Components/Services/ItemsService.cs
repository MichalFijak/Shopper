using Google.Cloud.Firestore;
using Shopper.Core.Components.Dtos;
using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Interfaces;
using Shopper.Data.Components.Webhooks;
using Shopper.Services.Components.Dtos;
using Shopper.Services.Components.Mappers;
using Shopper.Services.Components.Policies;
using Shopper.Services.Components.State;
using System.Diagnostics;

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

        public event Action OnItemsChanged
    {
        add => state.OnChange += value;
        remove => state.OnChange -= value;
    }


    public async Task<List<ItemGroupDto>> RefreshItemsAsync()
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
    public async Task MoveToCartAsync(ItemDto item)
    {
        var model = item.ConvertToModel();
        model.InCart =! model.InCart;
        var path = $"ItemList/{model.Name}";
        await firebaseWebhookHandler.UpdateItemAsync(path, model);
    }
    public async Task ModifyItemAsync(ItemDto item)
    {
        var oldItem = state.GetItemToModify();
        if (oldItem == null)
        {
            throw new InvalidOperationException("No item selected for modification.");
        }

        var model = item.ConvertToModel();

        var oldPath = $"ItemList/{oldItem.Name}";
        var newPath = $"ItemList/{model.Name}";

        if (oldItem.Name == item.Name)
        {
            await firebaseWebhookHandler.UpdateItemAsync(oldPath, model);
        }
        else
        {
            await firebaseWebhookHandler.DeleteItemAsync(oldPath);
            await firebaseWebhookHandler.CreateItemAsync(newPath, model);
        }
    }
    public string GetSelectedList() => state.GetSelectedList();

    public void SetSelectedList(string list) => state.SetSelectedList(list);

    public async Task StartRealtimeSyncAsync()
    {
        firebaseListener.ItemsUpdated += OnItemsUpdated;

        firebaseEventSource.ItemChanged += (item, changeType) =>
        {
            if (changeType == DocumentChange.Type.Removed)
            {
                firebaseListener.HandleItemRemoved(item);
            }
            else
            {
                firebaseListener.HandleItems(new List<ItemModel> { item });
            }
        };
        await firebaseEventSource.ListenToEventAsync("ItemList");
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

    public void SetItemToModify(ItemDto item)
    {
        state.SetItemToModify(item);

    }

    public List<ItemGroupDto> GetCachedItems()
    {
        var items = new List<ItemDto>(state.GetItems());
        Debug.WriteLine($"GetCachedItems: {items.Count} items");
        return cartPolicy.PartitionByCartStatus(items);

    }
}
