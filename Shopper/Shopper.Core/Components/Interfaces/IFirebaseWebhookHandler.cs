using Shopper.Core.Components.Entity;

namespace Shopper.Core.Components.Interfaces
{
    public interface IFirebaseWebhookHandler
    {
        Task CreateItemAsync(string path, ItemModel data);
        Task DeleteItemAsync(string path);
        Task<ItemModel> GetItemAsync(string path);
        Task UpdateItemAsync(string path, ItemModel data);
        Task<List<ItemModel>> GetAllItemsAsync(); 
    }
}