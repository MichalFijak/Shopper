
using Shopper.Services.Components.Dtos;

namespace Shopper.Core.Components.Dtos
{
    public class ItemGroupDto
    {
        public bool InCart { get; set; }
        public List<ItemDto> Items { get; set; } = new();
    }
}
