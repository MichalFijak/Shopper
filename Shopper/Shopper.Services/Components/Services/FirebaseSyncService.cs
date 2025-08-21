

using Google.Cloud.Firestore;
using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Factory;
using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.Services
{
    public class FirebaseSyncService : IFirebaseSyncService
    {
        private readonly FirestoreDb _db;
        private readonly string _collectionPath = "Actual_List";

        public FirebaseSyncService(IFirestoreClientFactory factory)
        {
            _db = factory.GetClient();
        }

        public async Task AddItemAsync(ItemDto item, int quantity)
        {
            var docRef = _db.Collection(_collectionPath).Document(item.Name);
            var model = new ItemModel
            {
                Description = new ItemModelDescription
                {
                    Genre = item.Genre,
                    Description = item.Description,
                    Price = item.Price
                },
                InCart = item.InCart,
                Name= item.Name
            };
            await docRef.SetAsync(model);
        }

        public async Task SubmitItemAsync(ItemDto item, int quantity)
        {
            var docRef = _db.Collection("SubmittedItems").Document(item.Name);
            await docRef.SetAsync(new { Quantity = quantity });
        }

        public async Task UpdateItemAsync(ItemDto item, int quantity)
        {
            var docRef = _db.Collection(_collectionPath).Document(item.Name);
            var model = new ItemModel
            {
                Description = new ItemModelDescription
                {
                    Genre = item.Genre,
                    Description = item.Description,
                    Price = item.Price
                },
                InCart = item.InCart,
                Name = item.Name
            };
            await docRef.SetAsync(model, SetOptions.MergeAll);
        }

        public async Task RemoveItemAsync(ItemDto item, int quantity)
        {
            var docRef = _db.Collection(_collectionPath).Document(item.Name);
            await docRef.DeleteAsync();
        }
    }
}
