

namespace Shopper.Core.Components.Enums
{
    public static class ItemCategoryExtensions
    {
        public static readonly Dictionary<ItemCategory, string> PolishNames = new()
        {
            { ItemCategory.MainList, "Lista główna" },
            { ItemCategory.Meat, "Mięso" },
            { ItemCategory.Wheat, "Zboża" },
            { ItemCategory.Dairy, "Nabiał" },
            { ItemCategory.Fruit, "Owoce" },
            { ItemCategory.Vegetables, "Warzywa" },
            { ItemCategory.Beverages, "Napoje" },
            { ItemCategory.Unknown, "Nieznane" },
            { ItemCategory.Cart, "Koszyk" }
        };
    }
}
