
namespace Shopper.Core.Components.Entity
{
    public class ItemModel
    {
        public string Name { get; set; } = string.Empty;
        public bool? InCart { get; set; } = false;

        public ItemModelDescription Description { get; set; } = new ItemModelDescription();
    }
}
