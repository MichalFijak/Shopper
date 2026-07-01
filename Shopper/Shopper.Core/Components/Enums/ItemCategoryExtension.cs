

namespace Shopper.Core.Components.Enums
{
    public static class ItemCategoryExtensions
    {
        public static readonly Dictionary<ItemCategory, string> PolishNames = new()
        {
            { ItemCategory.MainList, "Lista główna" },
            { ItemCategory.Wheat, "Zboża" },
            { ItemCategory.Dairy, "Nabiał" },
            { ItemCategory.Fruit, "Owoce" },
            { ItemCategory.Vegetables, "Warzywa" },
            { ItemCategory.Beverages, "Napoje" },
            { ItemCategory.Meat, "Mięso" },
            { ItemCategory.Cosmetics, "Kosmetyki" },
            { ItemCategory.Chemicals, "Chemikalia" },
            { ItemCategory.Unknown, "Nieznane" },
            { ItemCategory.Cart, "Koszyk" }
        };
    }
}
