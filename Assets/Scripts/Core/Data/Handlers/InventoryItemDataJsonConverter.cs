using System;

namespace Core.Data.Handlers
{
internal class InventoryItemDataJsonConverter : TypedGameDataJsonConverter<InventoryItemData>
{
    protected override (string, Type)[] GetMapEntities()
    {
        return new[]
        {
            ("armor", typeof(ArmorInventoryItemData)),
            ("consumable", typeof(ConsumableInventoryItemData)),
            ("weapon", typeof(WeaponInventoryItemData)),
        };
    }
}
}