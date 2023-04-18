using System.Linq;
using Core.Models;
using UnityEngine;

namespace Ui
{
internal class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryView view;
    
    private IInventory _inventory;
    private InventorySlotController _moveSelectionSrc;
    private bool SlotsAlreadyUnlocked => _inventory.Slots.All(slot => slot.IsActive);

    public void Setup(IInventory inventory)
    {
        _inventory = inventory;
        view.Setup(_inventory, !SlotsAlreadyUnlocked);
        view.SlotSelected += OnSlotSelected;
        view.UnlockSlotsBtnClick += OnUnlockSlotsBtnClick;
    }

    private void OnSlotSelected(InventorySlotController slotController)
    {
        if (!slotController.Slot.IsActive)
        {
            view.ShowUnlockBtn();
            return;
        }
        
        if (!_moveSelectionSrc)
        {
            _moveSelectionSrc = slotController;
            slotController.SetSelected(true);
            return;
        }

        if (_moveSelectionSrc == slotController)
        {
            _moveSelectionSrc.SetSelected(false);
            _moveSelectionSrc = null;
            return;
        }

        MoveItem(slotController);
    }

    private void MoveItem(InventorySlotController destSlotController)
    {
        var srcSlot = _moveSelectionSrc.Slot;
        if (srcSlot.Item != null)
        {
            _inventory.TryMove(srcSlot.SlotIndex, destSlotController.Slot.SlotIndex);
        }

        _moveSelectionSrc.SetSelected(false);
        _moveSelectionSrc = null;
    }

    private void OnUnlockSlotsBtnClick()
    {
        if (SlotsAlreadyUnlocked)
        {
            Debug.LogError("Slots are already unlocked!");
            return;
        }

        if (!TryChargePrice())
        {
            return;
        }

        UnlockSlots();
    }

    private void UnlockSlots()
    {
        var inventorySize = _inventory.Size;
        var inventoryInitUnlockedSlotsAmount = _inventory.InitiallyUnlockedSlotsAmount;
        var slotsToUnlockStartIndex = inventorySize - inventoryInitUnlockedSlotsAmount;
        for (int i = slotsToUnlockStartIndex; i < inventorySize; i++)
        {
            var inventorySlot = _inventory.Slots[i];
            inventorySlot.SetActive(true);
        }
    }

    private bool TryChargePrice()
    {
        Debug.Log("Price for slots unlock is paid");
        return true;
    }
}
}