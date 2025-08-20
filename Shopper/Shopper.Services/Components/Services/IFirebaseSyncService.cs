using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.Services
{
    public interface IFirebaseSyncService
    {
        public Task AddItemAsync(ItemDto item, int quantity);

        public Task SubmitItemAsync(ItemDto item, int quantity);

        public Task RemoveItemAsync(ItemDto item, int quantity);

        public Task UpdateItemAsync(ItemDto item, int quantity);
    }
}