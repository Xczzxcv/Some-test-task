using System;
using Core.Data;
using Infrastructure;
using UnityEngine;

namespace Core.Models
{
internal class InventorySlot : IInventorySlot
{
    public IInventoryItem Item { private set; get; }
    public int SlotIndex { get; }
    public bool IsActive
    {
        get => _data.IsActive;
        private set => _data.IsActive = value;
    }

    public event Action SlotUpdated;

    private readonly InventorySlotData _data;
    private readonly IInventoryItemsFactory _inventoryItemsFactory;

    public InventorySlot(InventorySlotData data, IInventoryItemsFactory inventoryItemsFactory,
        int slotIndex)
    {
        _data = data;
        _inventoryItemsFactory = inventoryItemsFactory;
        SlotIndex = slotIndex;
    }

    public void Init()
    {
        if (_data.Item == null)
        {
            SetItem(null);
            return;
        }

        SetNewItem(_inventoryItemsFactory.Create(_data.Item));
    }

    private void SetItem(IInventoryItem item)
    {
        if (item == Item)
        {
            return;
        }

        var oldItem = Item;
        Item = item;
        Debug.Log($"Slot {SlotIndex} item updated {oldItem} -> {Item}");
        
        SlotUpdated?.Invoke();
    }

    private void SetNewItem(IInventoryItem item)
    {
        SetItem(item);
        Subscribe();
    }

    private void RemoveItem()
    {
        Unsubscribe();
        SetItem(null);
        _data.Item = null;
    }

    private void Subscribe()
    {
        if (Item != null)
        {
            Item.AmountUpdated += OnItemAmountUpdated;
        }
    }

    private void Unsubscribe()
    {
        if (Item != null)
        {
            Item.AmountUpdated -= OnItemAmountUpdated;
        }
    }

    private void OnItemAmountUpdated()
    {
        if (Item?.Amount <= 0)
        {
            RemoveItem();
        }

        SlotUpdated?.Invoke();
    }

    public int CanPutIntoAmount(IInventoryItem item)
    {
        if (!IsActive)
        {
            return 0;
        }
        
        if (Item == null)
        {
            return item.Amount;
        }
        
        if (Item.Id != item.Id)
        {
            return 0;
        }

        var canFitAmount = Item.MaxStackAmount - Item.Amount;
        return canFitAmount;
    }

    public int TryPutItem(IInventoryItem item)
    {
        var canPutIntoAmount = CanPutIntoAmount(item);
        if (canPutIntoAmount <= 0)
        {
            return 0;
        }

        if (Item == null)
        {
            _data.Item = item.GetData();
            SetNewItem(item);
        }
        else
        {
            var oldAmount = Item.Amount;
            var actualMovingAmount = Mathf.Min(item.Amount, canPutIntoAmount);
            Item.IncreaseAmount(actualMovingAmount);
            item.DecreaseAmount(actualMovingAmount);
            Debug.Log($"Slot {SlotIndex}: item '{Item.Id}' amount updated " +
                      $"{oldAmount} -> {Item.Amount}");
        }
        
        return canPutIntoAmount;
    }

    public bool TryTakeAway(int amount, out IInventoryItem item)
    {
        Debug.Assert(amount >= 0);

        item = default;
        if (!IsActive)
        {
            return false;
        }
        
        if (amount == 0)
        {
            return true;
        }

        if (Item == null)
        {
            return false;
        }

        if (amount > Item.Amount)
        {
            return false;
        }

        if (amount < Item.Amount)
        {
            item = _inventoryItemsFactory.Create(Item.Id, amount);
            Item.DecreaseAmount(amount);
        }
        else if (amount == Item.Amount)
        {
            item = Item;
            RemoveItem();
        }

        return true;
    }

    public bool TryTakeAway(out IInventoryItem item)
    {
        return TryTakeAway(Item?.Amount ?? 1, out item);
    }

    public void SetActive(bool active)
    {
        if (IsActive == active)
        {
            return;
        }

        IsActive = active;
        SlotUpdated?.Invoke();
    }

    public override string ToString()
    {
        return $"Inv Slot({SlotIndex}) Item: {Item?.ToString() ?? "-"}";
    }
}
}