using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Core.Components.Interfaces
{
    public interface IAppEventPublisher
    {
        void PublishEvent<T>(T eventData) where T : class;
    }
}
