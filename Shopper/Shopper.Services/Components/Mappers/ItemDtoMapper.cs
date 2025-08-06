using Shopper.Core.Components.Entity;
using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.Mappers
{
    public static class ItemDtoMapper
    {
        public static ItemDto ToDto(this ItemModel item)
        {
            return new ItemDto
            {
                Name = item.Name,
                Genre = item.Genre,
                Description = item.Description,
                Price = item.Price
            };
        }
        public static ItemModel ToModel(this ItemDto itemDto)
        {
            return new ItemModel
            {
                Name = itemDto.Name,
                Genre = itemDto.Genre,
                Description = itemDto.Description,
                Price = itemDto.Price
            };
        }
    }

    // Think how you could also send request backwards to the server to update the item in the cart
}
