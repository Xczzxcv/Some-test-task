using Core.Data;
using Infrastructure;

namespace Core.Models
{
internal class InventorySlot : IInventorySlot
{
    public IInventoryItem Item { private set; get; }

    private readonly InventorySlotData _data;
    private readonly IInventoryItemFactory _inventoryItemFactory;

    public InventorySlot(InventorySlotData data)
    {
        _data = data;
    }

    public void Init()
    {
        Item = _data.Item != null
            ? _inventoryItemFactory.Create(_data.Item)
            : null;
    }

    public bool CanPutInto(IInventoryItem item)
    {
        throw new System.NotImplementedException();
    }

    public bool TryPutInto(IInventoryItem item)
    {
        throw new System.NotImplementedException();
    }

    public bool TryTakeAway(out IInventoryItem item)
    {
        throw new System.NotImplementedException();
    }
}
}