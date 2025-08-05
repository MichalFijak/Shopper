using FireSharp.Interfaces;
using Shopper.Core.Components.Factory;
using Shopper.Data.Components.Webhooks;


namespace Shopper.Data.Infrastructure.Firebase.Listeners
{
    public class FirebaseEventSource : IFirebaseEventSource
    {
        private readonly IFirebaseClient _firebaseClient;
        public FirebaseEventSource(IFirebaseClientFactory firebaseClientFactory)
        {
            _firebaseClient = firebaseClientFactory.Create();
        }

        public async Task ListenToEventAsync(string webhookData)
        {
            await _firebaseClient.OnAsync(webhookData);
            Console.WriteLine($"Handling Firebase webhook data: {webhookData}");
        }
    }
}
