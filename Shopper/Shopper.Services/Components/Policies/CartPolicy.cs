using Shopper.Core.Components.Dtos;
using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.Policies
{
    public class CartPolicy : ICartPolicy
    {
        public List<ItemGroupDto> PartitionByCartStatus(List<ItemDto> items)
        {
            var inCart = items.Where(i => i.InCart).ToList();
            var notInCart = items.Where(i => !i.InCart).ToList();
            return new List<ItemGroupDto>
            {
                new ItemGroupDto { InCart = false, Items = notInCart },
                new ItemGroupDto { InCart = true, Items = inCart }
            };
        }
    }
}
