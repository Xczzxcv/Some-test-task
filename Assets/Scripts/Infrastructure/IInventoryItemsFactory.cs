using Core.Data;
using Core.Models;

namespace Infrastructure
{
internal interface IInventoryItemsFactory
{
    IInventoryItem Create(InventoryItemData data);
    IInventoryItem Create(string itemId, int itemAmount);
}
}