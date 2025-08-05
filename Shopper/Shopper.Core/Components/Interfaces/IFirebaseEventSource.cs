
namespace Shopper.Data.Components.Webhooks
{
    public interface IFirebaseEventSource
    {
        Task ListenToEventAsync(string webhookData);
    }
}