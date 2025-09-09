
using Shopper.Core.Components.Dtos;
using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.Policies
{
    public class CartPolicy : ICartPolicy
    {
    public List<ItemGroupDto> PartitionByCartStatus(Dictionary<ItemDto, int> items)
        {
            var inCart = items
                .Where(kvp => kvp.Key.InCart)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var notInCart = items
                .Where(kvp => !kvp.Key.InCart)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return new List<ItemGroupDto>
        {
            new ItemGroupDto { InCart = true, Items = inCart },
            new ItemGroupDto { InCart = false, Items = notInCart }
        };
        }
    }
}
