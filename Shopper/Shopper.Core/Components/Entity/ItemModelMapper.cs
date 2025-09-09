using Shopper.Core.Components.Entity;
using System.Diagnostics;

namespace Shopper.Core.Components.Entity
{
    public static class ItemModelMapper
    {
        public static ItemModel? FromDictionary(Dictionary<string, object> itemMap)
        {
            try
            {
                var description = itemMap.TryGetValue("Description", out var descVal) && descVal is Dictionary<string, object> descMap
                    ? new ItemModelDescription
                    {
                        Genre = descMap.TryGetValue("Genre", out var genreVal) ? genreVal?.ToString() : null,
                        Description = descMap.TryGetValue("Description", out var descriptionVal) ? descriptionVal?.ToString() : null,
                        Price = descMap.TryGetValue("Price", out var priceVal) && decimal.TryParse(priceVal?.ToString(), out var price) ? price : null
                    }
                    : null;

                return new ItemModel
                {
                    Name = itemMap.TryGetValue("Name", out var nameVal) ? nameVal?.ToString() : null,
                    InCart = itemMap.TryGetValue("InCart", out var inCartVal) && Convert.ToBoolean(inCartVal),
                    Description = description
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error parsing item: {ex.Message}");
                return null;
            }
        }
    }
}
