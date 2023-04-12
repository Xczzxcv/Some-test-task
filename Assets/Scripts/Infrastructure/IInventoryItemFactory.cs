using Core.Configs;
using Core.Data;
using Core.Models;

namespace Infrastructure
{
internal interface IInventoryItemFactory
{
    public IInventoryItem Create(InventoryItemData data);
}
}