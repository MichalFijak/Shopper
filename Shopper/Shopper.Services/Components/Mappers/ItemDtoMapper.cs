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
                Genre = item.Description?.Genre,
                Description = item.Description?.Description,
                Price = item.Description?.Price,
                InCart = item.InCart
            };
        }
        public static ItemModel ToModel(this ItemDto itemDto)
        {
            return new ItemModel
            {
                Name = itemDto.Name,
                InCart = itemDto.InCart,
                Description = itemDto.ToDescription()
            };
        }

        private static ItemModelDescription ToDescription(this ItemDto itemDto)
        {
            return new ItemModelDescription
            {
                Genre = itemDto.Genre,
                Description = itemDto.Description,
                Price = itemDto.Price
            };
        }
    }

}
