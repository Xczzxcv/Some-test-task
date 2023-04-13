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

    private readonly InventoryConfig _config;
    private readonly InventoryData _dataPiece;
    private readonly List<IInventorySlot> _slots;

    public Inventory(InventoryConfig config, InventoryData data, 
        IInventoryItemFactory inventoryItemFactory)
    {
        _config = config;
        _dataPiece = data;
        _slots = new List<IInventorySlot>(_config.Size);
        for (var i = 0; i < _config.Size; i++)
        {
            var slotData = _dataPiece.Slots[i];
            var slot = new InventorySlot(slotData, inventoryItemFactory);
            _slots.Add(slot);
        }
    }

    public void Init()
    {
        Debug.Assert(_config.Size == _dataPiece.Slots.Count);

        foreach (var slot in _slots)
        {
            slot.Init();
        }
    }

    public bool CanPutInto(IInventoryItem item)
    {
        throw new System.NotImplementedException();
    }

    public bool TryPutInto(IInventoryItem item)
    {
        throw new System.NotImplementedException();
    }

    public bool TryTakeAway(string itemId, int amount, out IInventoryItem item)
    {
        throw new System.NotImplementedException();
    }

    public int GetAmount(string itemId)
    {
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
}
}