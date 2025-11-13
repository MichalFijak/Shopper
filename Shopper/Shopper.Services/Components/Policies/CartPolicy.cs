using Shopper.Core.Components.Dtos;
using Shopper.Services.Components.Dtos;
using System.Diagnostics;

namespace Shopper.Services.Components.Policies
{
    public class CartPolicy : ICartPolicy
    {
        public List<ItemGroupDto> PartitionByCartStatus(List<ItemDto> items)
        {
            var inCart = items.Where(i => i.InCart).ToList();
            var notInCart = items.Where(i => !i.InCart).ToList();
            Debug.WriteLine($"Partitioned: {notInCart.Count} not in cart, {inCart.Count} in cart");
            return new List<ItemGroupDto>
            {
                new ItemGroupDto { InCart = false, Items = notInCart },
                new ItemGroupDto { InCart = true, Items = inCart }
            };
        }
    }
}
