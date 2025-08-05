using FireSharp.Interfaces;


namespace Shopper.Core.Components.Factory
{
    public interface IFirebaseClientFactory
    {
        IFirebaseClient Create();
    }
}
