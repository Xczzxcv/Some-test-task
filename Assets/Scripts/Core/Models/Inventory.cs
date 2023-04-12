using System.Collections.Generic;
using Core.Configs;
using Core.Data;
using UnityEngine;

namespace Core.Models
{
internal class Inventory : IInventory
{
    public IReadOnlyList<IInventorySlot> Slots => _slots;

    private readonly InventoryConfig _config;
    private readonly InventoryData _data;
    private readonly List<IInventorySlot> _slots;

    public Inventory(InventoryConfig config, InventoryData data)
    {
        _config = config;
        _data = data;
        _slots = new List<IInventorySlot>(_config.Size);
    }

    public void Init()
    {
        Debug.Assert(_config.Size == _data.Slots.Count);

        for (var i = 0; i < _config.Size; i++)
        {
            var slotData = _data.Slots[i];
            var slot = new InventorySlot(slotData);
            _slots.Add(slot);
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

    public bool TryTakeAway(string itemId, out IInventoryItem item)
    {
        throw new System.NotImplementedException();
    }
}
}