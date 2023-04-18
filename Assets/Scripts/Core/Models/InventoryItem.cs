using System;
using Core.Configs;
using Core.Data;
using UnityEngine;

namespace Core.Models
{
internal abstract class InventoryItem<TConfig, TData> : IInventoryItem
    where TConfig : InventoryItemConfig
    where TData : InventoryItemData
{
    public string Id => Config.Id;
    public int Amount => Data.Amount;
    public int MaxStackAmount => Config.MaxStackAmount;
    public event Action AmountUpdated;

    protected readonly TConfig Config;
    protected readonly TData Data;

    protected InventoryItem(TConfig config, TData data)
    {
        Config = config;
        Data = data;
    }

    public void IncreaseAmount(int amount)
    {
        Debug.Assert(MaxStackAmount - Amount >= amount);

        UpdateAmount(Amount + amount);
    }

    public void DecreaseAmount(int amount)
    {
        Debug.Assert(Amount >= amount);

        UpdateAmount(Amount - amount);
    }

    private void UpdateAmount(int newValue)
    {
        Debug.Assert(0 <= newValue && newValue <= MaxStackAmount);

        Data.Amount = newValue;
        AmountUpdated?.Invoke();
    }

    public InventoryItemData GetData() => Data;

    public override string ToString() => $"Inv Item '{Id}' ({Amount}/{MaxStackAmount})";
}
}