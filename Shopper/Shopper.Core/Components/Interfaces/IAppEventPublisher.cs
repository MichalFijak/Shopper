
namespace Shopper.Core.Components.Interfaces
{
    public interface IAppEventPublisher
    {
        void PublishEvent<T>(T eventData) where T : class;
    }
}
