namespace Core.Configs
{
internal interface IConfigsProvider
{
    IReadOnlyConfigCollection<InventoryItemConfig> InventoryItems { get; }
    IReadOnlyConfigCollection<InventoryItemViewConfig> InventoryItemViews { get; }
    InventoryConfig Inventory { get; }
}
}