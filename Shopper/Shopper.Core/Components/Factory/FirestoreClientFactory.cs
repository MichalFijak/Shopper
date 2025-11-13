
using Google.Cloud.Firestore;
using Microsoft.Extensions.Options;
using Shopper.Core.Components.Configs;

namespace Shopper.Core.Components.Factory
{
    public class FirestoreClientFactory : IFirestoreClientFactory
    {
        private readonly FirestoreDb _firestoreDb;

        public FirestoreClientFactory(IOptions<FirestoreOptions> options)
        {
            var config = options.Value;
            var content = "";
            using(var stream = new FileStream(config.CredentialPath, FileMode.Open, FileAccess.Read))
            {
                content = new StreamReader(stream).ReadToEnd();
            }

            _firestoreDb = new FirestoreDbBuilder
            {
                ProjectId = config.ProjectId,
                JsonCredentials = content,
            }.Build();
        }

        public FirestoreDb GetClient()
        {
            return _firestoreDb;
        }
    }
}
