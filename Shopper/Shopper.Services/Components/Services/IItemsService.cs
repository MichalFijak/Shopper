using Shopper.Core.Components.Dtos;
using Shopper.Services.Components.Dtos;

public interface IItemsService
{
    Task AddItemAsync(ItemDto item);
    Task<List<ItemGroupDto>> GetItemsAsync();
    ItemDto GetItemToModify();
    string GetSelectedList();
    Task RemoveItemAsync(ItemDto item);
    void SetItemToModify(ItemDto item);
    void SetSelectedList(string list);
    Task StartRealtimeSyncAsync();
    void StopRealtimeSync();
}

