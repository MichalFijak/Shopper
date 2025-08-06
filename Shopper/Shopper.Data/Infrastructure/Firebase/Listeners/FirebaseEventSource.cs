using Google.Cloud.Firestore;
using Shopper.Core.Components.Entity;
using Shopper.Core.Components.Factory;
using Shopper.Data.Components.Webhooks;
using Shopper.Data.Infrastructure.Firebase.Webhooks;


namespace Shopper.Data.Infrastructure.Firebase.Listeners
{
    public class FirebaseEventSource : IFirebaseEventSource
    {
        private readonly FirestoreDb firebaseClient;
        private readonly IFirebaseWebhookHandler firebaseWebhookHandler;

        public FirebaseEventSource(IFirestoreClientFactory firebaseClientFactory, IFirebaseWebhookHandler firebaseWebhookHandler)
        {
            this.firebaseClient = firebaseClientFactory.Create();
            this.firebaseWebhookHandler = firebaseWebhookHandler;
        }

        public async Task ListenToEventAsync(string webhookData)
        {
            CollectionReference listRef = firebaseClient.Collection("sharedList");

            listRef.Listen(snapshot =>
            {
                foreach (DocumentChange change in snapshot.Changes)
                {
                    Console.WriteLine($"Change Type: {change.ChangeType}");
                    Console.WriteLine($"Document ID: {change.Document.Id}");

                    switch (change.ChangeType)
                    {
                        case DocumentChange.Type.Added:
                            Console.WriteLine("Item about to being added.");

                            firebaseWebhookHandler.CreateItemAsync(change.Document.Reference.Path, change.Document.ConvertTo<ItemModel>()).Wait();
                            Console.WriteLine("Item added.");
                            break;

                        case DocumentChange.Type.Modified:
                            Console.WriteLine("Item about to being modified.");

                            firebaseWebhookHandler.UpdateItemAsync(change.Document.Reference.Path, change.Document.ConvertTo<ItemModel>()).Wait();
                            Console.WriteLine("Item modified.");
                            break;

                        case DocumentChange.Type.Removed:
                            Console.WriteLine("Item about to being removed.");
                            firebaseWebhookHandler.DeleteItemAsync(change.Document.Reference.Path).Wait();
                            Console.WriteLine("Item removed.");
                            break;
                    }
                }
            });

            await Task.CompletedTask;
        }
    }
}
