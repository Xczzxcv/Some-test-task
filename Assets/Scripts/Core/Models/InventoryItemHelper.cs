namespace Core.Models
{
internal static class InventoryItemHelper
{
    private class MockInventoryItem : IInventoryItem
    {
        public string Id => "mock_item";
        public int Amount => 1;
    }
    
    public static IInventoryItem CreateMockItem()
    {
        return new MockInventoryItem();
    }
}
}