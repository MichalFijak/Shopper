
using Google.Cloud.Firestore;

namespace Shopper.Core.Components.Factory
{
    public class FirestoreClientFactory : IFirestoreClientFactory
    {
        private readonly FirestoreDb _firestoreDb;

        public FirestoreClientFactory(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        public FirestoreDb GetClient()
        {
            return _firestoreDb;
        }
    }
}
