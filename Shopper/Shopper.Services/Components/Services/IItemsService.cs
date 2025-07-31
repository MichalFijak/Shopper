using Shopper.Components.Entity;

public interface IItemsService
{
    void AddItem(ItemDto item, int quantity);
    ItemDto GetItemToModify();
    Dictionary<ItemDto, int> GetShoppingList();
    Dictionary<ItemDto, int> GetSubmittedItems();
    void ModifyItem(ItemDto item, ItemDto newitem, int amount);
    void RemoveItem(ItemDto item, int quantity);
    void SetItemToModify(ItemDto item);
    void SubmitItem(ItemDto item, int quantity);
}