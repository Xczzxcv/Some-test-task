using System;
using Core.Data;

namespace Core.Models
{
internal static class InventoryItemHelper
{
    private class MockInventoryItem : IInventoryItem
    {
        public string Id => MockItemId;
        private const string MockItemId = "mock_item";
        public int Amount => 1;
        public int MaxStackAmount => 13;
        public event Action AmountUpdated;
        public void IncreaseAmount(int amount)
        { }
        public void DecreaseAmount(int amount)
        { }
        public InventoryItemData GetData() => new ArmorInventoryItemData(MockItemId, 1);
    }
    
    public static IInventoryItem CreateMockItem()
    {
        return new MockInventoryItem();
    }
}
}