using Google.Cloud.Firestore;
using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Factory;
using Shopper.Core.Components.Interfaces;

namespace Shopper.Data.Infrastructure.Firebase.Webhooks
{
    public class FirebaseWebhookHandler(IFirestoreClientFactory firebaseClientFactory) : IFirebaseWebhookHandler
    {
        private readonly FirestoreDb firebaseClient = firebaseClientFactory.GetClient();

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

        public async Task<List<ItemModel>> GetAllItemsAsync()
        {
            var items = new List<ItemModel>();
            var itemListCollection = firebaseClient.Collection("ItemList");
            var snapshot = await itemListCollection.GetSnapshotAsync();

            foreach (var doc in snapshot.Documents)
            {
                if (doc.Exists)
                {
                    items.Add(doc.ConvertTo<ItemModel>());
                }
            }

            return items;
        }
    }

}
