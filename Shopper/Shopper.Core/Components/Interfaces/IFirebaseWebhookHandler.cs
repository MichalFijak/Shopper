using Shopper.Core.Components.Entity;

namespace Shopper.Data.Infrastructure.Firebase.Webhooks
{
    public interface IFirebaseWebhookHandler
    {
        Task CreateItemAsync(string path, ItemModel data);
        Task DeleteItemAsync(string path);
        Task<ItemModel> GetItemAsync(string path);
        Task UpdateItemAsync(string path, ItemModel data);
    }
}