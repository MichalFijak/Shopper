
using Google.Cloud.Firestore;
using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Factory;
using Shopper.Services.Components.Dtos;
using Shopper.Services.Components.Mappers;

namespace Shopper.Services.Components.Services
{
    public class FirebaseSyncService : IFirebaseSyncService
    {
        private readonly FirestoreDb _db;
        private readonly string _collectionPath = "ItemList";
        private readonly string _documentPath = "ShopList";
        public FirebaseSyncService(IFirestoreClientFactory factory)
        {
            _db = factory.GetClient();
        }

        public async Task AddItemAsync(ItemDto item, int quantity)
        {
            var docRef = _db.Collection(_collectionPath).Document(_documentPath);
            
            var model = item.ConvertToModel();
            var result = new Dictionary<string, ItemModel> { { item.Name, model } };
            await docRef.SetAsync(result, SetOptions.MergeAll);
        }

        public async Task SubmitItemAsync(ItemDto item, int quantity)
        {
            var docRef = _db.Collection(_collectionPath).Document(_documentPath);
            item.InCart= true;
            var submittedItem = new Dictionary<string, object> { { item.Name, item.ConvertToModel() } };
            await docRef.SetAsync(submittedItem, SetOptions.MergeAll);
        }

        public async Task UpdateItemAsync(ItemDto item, int quantity)
        {
            var docRef = _db.Collection(_collectionPath).Document(_documentPath);
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
            var docRef = _db.Collection(_collectionPath).Document(_documentPath);
            var updates = new Dictionary<string, object>
            {
                {item.Name,FieldValue.ArrayRemove(item) }
            };
            
            await docRef.UpdateAsync(updates);
        }
        public async Task<Dictionary<ItemDto, int>> GetAllItemsAsync()
        {
            var result = new Dictionary<ItemDto, int>();

            try
            {
                var snapshot = await _db.Collection(_collectionPath)
                                        .Document(_documentPath)
                                        .GetSnapshotAsync();

                if (snapshot.Exists)
                {
                    var shopListMap = snapshot.ToDictionary();

                    foreach (var itemEntry in shopListMap)
                    {
                        var itemData = itemEntry.Value as Dictionary<string, object>;
                        if (itemData == null || !itemData.ContainsKey("Description")) continue;

                        var descriptionData = itemData["Description"] as Dictionary<string, object>;
                        if (descriptionData == null) continue;

                        var dto = new ItemDto
                        {
                            Name = itemData["Name"]?.ToString(),
                            Description = descriptionData["Description"]?.ToString(),
                            Genre = descriptionData["Genre"]?.ToString(),
                            Price = Convert.ToInt32(descriptionData["Price"]),
                            InCart = Convert.ToBoolean(itemData["InCart"])
                        };

                        result[dto] = 1; // You can adjust quantity logic as needed
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Firestore error: {ex}");
            }

            return result;
        }
    }
}
