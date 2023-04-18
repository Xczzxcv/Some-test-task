using System.Collections.Generic;
using Core.Configs;
using Core.Data;
using Infrastructure;
using UnityEngine;

namespace Core.Models
{
internal class Inventory : IInventory
{
    public IReadOnlyList<IInventorySlot> Slots => _slots;
    public int Size => _config.Size;
    public int InitiallyUnlockedSlotsAmount => _config.InitiallyUnlockedSlotsAmount;
    public float UnlockSlotsPrice => _config.UnlockPrice;

    private readonly InventoryConfig _config;
    private readonly InventoryData _data;
    private readonly List<IInventorySlot> _slots;

    public Inventory(InventoryConfig config, InventoryData data, 
        IInventoryItemsFactory inventoryItemsFactory)
    {
        _config = config;
        _data = data;
        _slots = new List<IInventorySlot>(_config.Size);
        for (var i = 0; i < _config.Size; i++)
        {
            var slotData = _data.Slots[i];
            var slot = new InventorySlot(slotData, inventoryItemsFactory, i);
            _slots.Add(slot);
        }
    }

    public void Init()
    {
        Debug.Assert(_config.Size == _data.Slots.Count);

        foreach (var slot in _slots)
        {
            slot.Init();
        }
    }

    public int CanPutIntoAmount(IInventoryItem item)
    {
        var canPutIntoAmountTotal = 0;
        foreach (var slot in _slots)
        {
            canPutIntoAmountTotal += slot.CanPutIntoAmount(item);
            if (canPutIntoAmountTotal >= item.Amount)
            {
                return item.Amount;
            }
        }

        return canPutIntoAmountTotal;
    }

    public int TryPutItem(IInventoryItem item)
    {
        var initItemAmount = item.Amount;
        var putItemAmount = 0;
        foreach (var slot in _slots)
        {
            putItemAmount += slot.TryPutItem(item);
            var remainAmount = initItemAmount - putItemAmount;
            if (remainAmount <= 0)
            {
                return initItemAmount;
            }
        }

        return putItemAmount;
    }

    public bool TryTakeAway(string itemId, int amount, out IInventoryItem item)
    {
        foreach (var slot in _slots)
        {
            if (slot.Item == null)
            {
                continue;
            }

            if (slot.Item.Id != itemId)
            {
                continue;
            }

            if (slot.Item.Amount < amount)
            {
                continue;
            }

            if (!slot.TryTakeAway(amount, out item))
            {
                continue;
            }

            return true;
        }

        item = default;
        return false;
    }

    public int GetAmount(string itemId)
    {
        Debug.Assert(!string.IsNullOrEmpty(itemId));
        
        var totalAmount = 0;
        foreach (var slot in _slots)
        {
            if (slot.Item?.Id == itemId)
            {
                totalAmount += slot.Item.Amount;
            }
        }

        return totalAmount;
    }

    public bool TryMove(int srcSlotIndex, int destSlotIndex)
    {
        var slotIndexesCheck = srcSlotIndex < 0 || srcSlotIndex >= _slots.Count
                                 || destSlotIndex < 0 || destSlotIndex >= _slots.Count;
        if (slotIndexesCheck)
        {
            return false;
        }

        if (srcSlotIndex == destSlotIndex)
        {
            return true;
        }

        var srcSlot = _slots[srcSlotIndex];
        var destSlot = _slots[destSlotIndex];

        var noEmptySpaceAtDestination = destSlot.Item?.Amount >= destSlot.Item?.MaxStackAmount;
        if (noEmptySpaceAtDestination)
        {
            return false;
        }

        var emptySrcSlot = srcSlot.Item == null;
        if (emptySrcSlot)
        {
            return true;
        }

        var emptyDestSlot = destSlot.Item == null;
        if (emptyDestSlot)
        {
            srcSlot.TryTakeAway(out var item);
            destSlot.TryPutItem(item);
            return true;
        }

        if (srcSlot.Item.Id != destSlot.Item.Id)
        {
            return false;
        }

        var putIntoAmount = destSlot.TryPutItem(srcSlot.Item);
        return putIntoAmount > 0;
    }
}
}