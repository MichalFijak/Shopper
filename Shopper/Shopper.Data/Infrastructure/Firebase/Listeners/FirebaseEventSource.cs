using Google.Cloud.Firestore;
using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Factory;
using Shopper.Data.Components.Webhooks;
using System.Diagnostics;


namespace Shopper.Data.Infrastructure.Firebase.Listeners
{
    public class FirebaseEventSource : IFirebaseEventSource, IAsyncDisposable
    {
        private readonly FirestoreDb firebaseClient;
        private readonly string collectionPath = "ItemList";
        private FirestoreChangeListener? listener;

        public event Action<ItemModel, DocumentChange.Type>? ItemChanged;

        public FirebaseEventSource(IFirestoreClientFactory firebaseClientFactory)
        {
            this.firebaseClient = firebaseClientFactory.GetClient();
        }

        public async Task ListenToEventAsync(string webhookData)
        {
            try
            {
                CollectionReference listRef = firebaseClient.Collection(collectionPath);
                listener = listRef.Listen(async snapshot =>
                {
                    try
                    {
                        foreach (DocumentChange change in snapshot.Changes)
                        {
                            Debug.WriteLine($"Change Type: {change.ChangeType}");
                            Debug.WriteLine($"Document ID: {change.Document.Id}");

                            var item = change.Document.ConvertTo<ItemModel>();
                            ItemChanged?.Invoke(item, change.ChangeType);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error processing Firestore changes: {ex}");
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting up Firestore listener: {ex}");
                throw;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (listener != null)
            {
                await listener.StopAsync();
            }
        }
    }
}
