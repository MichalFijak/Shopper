using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.Policies
{
    public interface ISegregationPolicy
    {
        List<ItemDto> GetItemsInCart(List<ItemDto> items);
        List<ItemDto> GetItemsNotInCart(List<ItemDto> items);
    }
}