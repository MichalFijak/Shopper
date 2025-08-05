using FireSharp.Interfaces;

namespace Shopper.Core.Components.Factory
{
    public class FirebaseClientFactory : IFirebaseClientFactory
    {
        public IFirebaseClient Create()
        {
            return new FireSharp.FirebaseClient(new FireSharp.Config.FirebaseConfig
            {
                AuthSecret = "your_auth_secret",
                BasePath = "https://your-firebase-database.firebaseio.com/"
            });
        }
    }
}
