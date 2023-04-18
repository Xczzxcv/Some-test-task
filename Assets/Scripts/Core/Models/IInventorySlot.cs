using System;
using JetBrains.Annotations;

namespace Core.Models
{
internal interface IInventorySlot
{
    [CanBeNull] IInventoryItem Item { get; }
    int SlotIndex { get; }
    bool IsActive { get; }
    event Action SlotUpdated;
    
    void Init();
    int CanPutIntoAmount(IInventoryItem item);
    int TryPutItem(IInventoryItem item);
    bool TryTakeAway(int amount, out IInventoryItem item);
    bool TryTakeAway(out IInventoryItem item);
    void SetActive(bool active);
}
}