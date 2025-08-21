using Google.Cloud.Firestore;


namespace Shopper.Core.Components.Factory
{
    public interface IFirestoreClientFactory
    {
        FirestoreDb GetClient();
    }
}
