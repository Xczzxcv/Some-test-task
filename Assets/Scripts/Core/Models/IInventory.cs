using System.Collections.Generic;

namespace Core.Models
{
internal interface IInventory
{
    IReadOnlyList<IInventorySlot> Slots { get; }

    void Init();
    bool CanPutInto(IInventoryItem item);

    bool TryPutInto(IInventoryItem item);
    
    bool TryTakeAway(string itemId, int amount, out IInventoryItem item);
    
    int GetAmount(string itemId);
}
}