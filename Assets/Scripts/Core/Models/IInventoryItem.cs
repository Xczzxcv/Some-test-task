using System;
using Core.Data;

namespace Core.Models
{
internal interface IInventoryItem
{
    string Id { get; }
    int Amount { get; }
    int MaxStackAmount { get; }
    event Action AmountUpdated;

    void IncreaseAmount(int amount);
    void DecreaseAmount(int amount);
    InventoryItemData GetData();
}
}