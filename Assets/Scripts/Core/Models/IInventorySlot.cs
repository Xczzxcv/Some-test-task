using JetBrains.Annotations;

namespace Core.Models
{
internal interface IInventorySlot
{
    [CanBeNull] IInventoryItem Item { get; }

    void Init();
    
    bool CanPutInto(IInventoryItem item);

    bool TryPutInto(IInventoryItem item);
    
    bool TryTakeAway(out IInventoryItem item);
}
}