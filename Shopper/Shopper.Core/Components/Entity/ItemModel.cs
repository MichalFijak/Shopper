
using Google.Cloud.Firestore;

namespace Shopper.Core.Components.Entity
{
    [FirestoreData]
    public class ItemModel
    {
        [FirestoreProperty]
        public string Name { get; set; } = string.Empty;
        [FirestoreProperty]

        public bool? InCart { get; set; } = false;
        [FirestoreProperty]

        public ItemModelDescription Description { get; set; } = new ItemModelDescription();
    }
}
