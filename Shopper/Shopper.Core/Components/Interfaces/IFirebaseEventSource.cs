
using Google.Cloud.Firestore;
using Shopper.Core.Components.Entity;

namespace Shopper.Data.Components.Webhooks
{
    public interface IFirebaseEventSource
    {
        event Action<ItemModel, DocumentChange.Type> ItemChanged;
        Task ListenToEventAsync(string webhookData);
    }
}