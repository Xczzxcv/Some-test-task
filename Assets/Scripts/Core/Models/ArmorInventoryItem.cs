using Core.Configs;
using Core.Data;

namespace Core.Models
{
internal class ArmorInventoryItem : InventoryItem<ArmorInventoryItemConfig, ArmorInventoryItemData>
{
    public ArmorInventoryItem(ArmorInventoryItemConfig config, ArmorInventoryItemData data) 
        : base(config, data)
    { }
}
}