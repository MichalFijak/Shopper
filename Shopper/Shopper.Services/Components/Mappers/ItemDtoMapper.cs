using Shopper.Core.Components.Entity;
using Shopper.Services.Components.Dtos;

namespace Shopper.Services.Components.Mappers
{
    public static class ItemDtoMapper
    {
        public static ItemDto ConvertToDto(this ItemModel item)
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
        public static ItemModel ConvertToModel(this ItemDto itemDto)
        {
            return new ItemModel
            {
                Name = itemDto.Name,
                InCart = itemDto.InCart,
                Description = itemDto.ToDescription()
            };
        }
        public static Dictionary<ItemDto, int> ConvertToDto(this Dictionary<ItemModel, int> items)
        {
            return items.ToDictionary(
                kvp => kvp.Key.ConvertToDto(),
                kvp => kvp.Value
            );
        }

        public static Dictionary<ItemModel, int> ConvertToModel(this Dictionary<ItemDto, int> items)
        {
            return items.ToDictionary(
                kvp => kvp.Key.ConvertToModel(),
                kvp => kvp.Value
            );
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
