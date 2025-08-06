using Google.Cloud.Firestore;
using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Factory;
using Shopper.Data.Components.Webhooks;

namespace Shopper.Data.Infrastructure.Firebase.Webhooks
{
    public class FirebaseWebhookHandler : IFirebaseWebhookHandler
    {
        private readonly FirestoreDb firebaseClient;
        public FirebaseWebhookHandler(IFirestoreClientFactory firebaseClientFactory)
        {
            this.firebaseClient = firebaseClientFactory.Create();

        }

        public async Task<ItemModel> GetItemAsync(string path)
        {
            var docRef = firebaseClient.Document(path);
            var snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                throw new Exception("Item not found.");
            }
            return snapshot.ConvertTo<ItemModel>();
        }

        public async Task CreateItemAsync(string path, ItemModel data)
        {
            var docRef = firebaseClient.Document(path);

            await docRef.CreateAsync(data);
        }

        public async Task UpdateItemAsync(string path, ItemModel data)
        {

            var docRef = firebaseClient.Document(path);

            await docRef.SetAsync(data, SetOptions.MergeAll);
        }

        public async Task DeleteItemAsync(string path)
        {

            DocumentReference docRef = firebaseClient.Document(path);
            await docRef.DeleteAsync();
        }

    }

}
