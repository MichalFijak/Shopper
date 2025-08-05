using Shopper.Core.Components.Entity;

namespace Shopper.Data.Components.Webhooks
{
    public interface IFirebaseWebhookService
    {
        Task CreateItemAsync(string path, ItemModel data);
        Task DeleteItemAsync(string path);
        Task<ItemModel> GetItemAsync(string path);
        Task UpdateItemAsync(string path, ItemModel data);
    }
}