
namespace Shopper.Core.Components.Entity
{
public class ItemModel
    {

        // this will represent an item in the shopping cart or a list from the endpoint
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Price { get; set; }

    }
}
