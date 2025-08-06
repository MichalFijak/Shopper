
using Google.Cloud.Firestore;

namespace Shopper.Core.Components.Factory
{
    public class FirestoreClientFactory : IFirestoreClientFactory
    {
        public FirestoreDb Create()
        {
            return FirestoreDb.Create("your-project-id");
        }
    }
}
