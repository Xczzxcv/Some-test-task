using Core.Configs;
using Core.Data;

namespace Core.Models
{
internal class ConsumableInventoryItem : InventoryItem<ConsumableInventoryItemConfig, ConsumableInventoryItemData>
{
    public ConsumableItemType ConsumableType => Config.ConsumableType;

    public ConsumableInventoryItem(ConsumableInventoryItemConfig config, ConsumableInventoryItemData data) 
        : base(config, data)
    { }
}
}