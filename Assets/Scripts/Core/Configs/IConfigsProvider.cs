namespace Core.Configs
{
internal interface IConfigsProvider
{
    IReadOnlyConfigCollection<InventoryItemConfig> InventoryItems { get; }
    InventoryConfig Inventory { get; }
}
}