using Shopper.Core.Components.Dtos;
using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.Policies
{
    public interface ICartPolicy
    {
        List<ItemGroupDto> PartitionByCartStatus(Dictionary<ItemDto, int> items);
    }
}