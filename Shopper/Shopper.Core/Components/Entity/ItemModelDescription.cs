
using Google.Cloud.Firestore;

namespace Shopper.Core.Components.Entity
{
    [FirestoreData]
    public class ItemModelDescription
    {
        [FirestoreProperty]
        public string? Genre { get; set; }
        
        [FirestoreProperty]
        public string? Description { get; set; }
        
        [FirestoreProperty]
        public string? Price { get; set; }

        [FirestoreProperty]
        public string? Amount { get; set; }
    }
}
