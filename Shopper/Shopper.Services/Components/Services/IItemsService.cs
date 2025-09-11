using Shopper.Core.Components.Dtos;
using Shopper.Services.Components.Dtos;

public interface IItemsService
{
    event Action OnItemsChanged;

    Task AddItemAsync(ItemDto item);

    Task ModifyItemAsync(ItemDto item);
    Task RemoveItemAsync(ItemDto item);
    void SetItemToModify(ItemDto item);
    void SetSelectedList(string list);
    Task MoveToCartAsync(ItemDto item);
    Task StartRealtimeSyncAsync();
    List<ItemGroupDto> GetCachedItems();
    Task<List<ItemGroupDto>> RefreshItemsAsync();

    string GetSelectedList();

    void StopRealtimeSync();
}

