using Shopper.Core.Components.Dtos;
using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.Policies
{
    public class CartPolicy : ICartPolicy
    {
        public List<ItemGroupDto> PartitionByCartStatus(List<ItemDto> items)
        {
            return items
                .GroupBy(i => i.InCart)
                .Select(g => new ItemGroupDto { InCart = g.Key, Items = g.ToList() })
                .ToList();
        }
    }
}
