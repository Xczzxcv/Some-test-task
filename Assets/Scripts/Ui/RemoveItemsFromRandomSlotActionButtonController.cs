using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ui
{
internal class RemoveItemsFromRandomSlotActionButtonController : ActionButtonController
{
    protected override void PerformAction()
    {
        var inventorySlots = Inventory.Slots;
        var nonEmptySlots = inventorySlots.Where(slot => slot.Item != null).ToArray();
        var randomSlotIndex = Random.Range(0, nonEmptySlots.Length);
        var randomSlot = nonEmptySlots[randomSlotIndex];
        if (!randomSlot.TryTakeAway(out _))
        {
            Debug.LogError("Can't remove any items from inventory");
        }
    }
}
}