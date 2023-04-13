using Core.Configs;
using Core.Data;

namespace Core.Models
{
internal class WeaponInventoryItem : InventoryItem<WeaponInventoryItemConfig, WeaponInventoryItemData>
{
    public int ProjectilesPerShot => Config.BulletsPerShot;
    public string ProjectileItemId => Config.ProjectileItemConfig.Id;
    
    public WeaponInventoryItem(WeaponInventoryItemConfig config, WeaponInventoryItemData data) 
        : base(config, data)
    { }
}
}