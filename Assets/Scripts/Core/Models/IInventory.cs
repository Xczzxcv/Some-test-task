using System.Collections.Generic;

namespace Core.Models
{
internal interface IInventory
{
    IReadOnlyList<IInventorySlot> Slots { get; }
    int Size { get; }
    int InitiallyUnlockedSlotsAmount { get; }
    float UnlockSlotsPrice { get; }

    void Init();
    int CanPutIntoAmount(IInventoryItem item);
    int TryPutItem(IInventoryItem item);
    bool TryTakeAway(string itemId, int amount, out IInventoryItem item);
    int GetAmount(string itemId);
    bool TryMove(int srcSlotIndex, int destSlotIndex);
}
}