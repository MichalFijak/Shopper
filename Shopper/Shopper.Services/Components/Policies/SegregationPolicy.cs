
using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.Policies
{
    public class SegregationPolicy : ISegregationPolicy
    {

        public List<ItemDto> GetItemsInCart(List<ItemDto> items)
        {
            return items.Where(item => item.InCart == true).ToList();
        }

        public List<ItemDto> GetItemsNotInCart(List<ItemDto> items)
        {
            return items.Where(item => item.InCart == false).ToList();
        }
    }
}
