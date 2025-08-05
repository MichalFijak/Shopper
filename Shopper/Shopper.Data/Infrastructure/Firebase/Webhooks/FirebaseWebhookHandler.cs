using FireSharp.Interfaces;
using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Factory;
using Shopper.Data.Components.Webhooks;

namespace Shopper.Data.Infrastructure.Firebase.Webhooks
{
    public class FirebaseWebhookService : IFirebaseWebhookService
    {
        private readonly IFirebaseClient _firebaseClient;

        public FirebaseWebhookService(IFirebaseClientFactory firebaseClientFactory)
        {
            _firebaseClient = firebaseClientFactory.Create();
        }

        public async Task<ItemModel> GetItemAsync(string path)
        {
            var response = await _firebaseClient.GetAsync(path);
            if (response.Body == null)
            {
                throw new Exception("Item not found.");
            }
            return response.ResultAs<ItemModel>();
        }

        public async Task CreateItemAsync(string path, ItemModel data)
        {
            await _firebaseClient.SetAsync(path, data);
        }

        public async Task UpdateItemAsync(string path, ItemModel data)
        {
            await _firebaseClient.UpdateAsync(path, data);
        }

        public async Task DeleteItemAsync(string path)
        {
            await _firebaseClient.DeleteAsync(path);
        }
    }

}
