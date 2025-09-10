using Shopper.Core.Components.Dtos;
using Shopper.Services.Components.Dtos;
using System.Collections.Generic;

namespace Shopper.Services.Components.Policies
{
    public interface ICartPolicy
    {
        List<ItemGroupDto> PartitionByCartStatus(List<ItemDto> items);
    }
}