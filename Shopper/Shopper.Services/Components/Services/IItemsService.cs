

using Shopper.Core.Components.Dtos;
using Shopper.Services.Components.Dtos;

public interface IItemsService
{
    Task AddItemAsync(ItemDto item, int quantity);
    Task<List<ItemGroupDto>> GetItems();
    Task<Dictionary<ItemDto, int>> GetItemsAsync();
    ItemDto GetItemToModify();
    string GetSelectedList();
    Task RemoveItemAsync(ItemDto item, int quantity);
    void SetItemToModify(ItemDto item);
    void SetSelectedList(string list);
    Task StartRealtimeSyncAsync();
    void StopRealtimeSync();
    Task SubmitItemAsync(ItemDto item, int quantity);
}

