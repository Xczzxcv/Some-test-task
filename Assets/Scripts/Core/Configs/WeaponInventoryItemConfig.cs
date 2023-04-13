using UnityEngine;

namespace Core.Configs
{
[CreateAssetMenu(menuName = "Configs/Inventory Item/Weapon", fileName = "WeaponInventoryItemConfig", order = 0)]
internal class WeaponInventoryItemConfig : InventoryItemConfig
{
    public float DamageAmount;
    public int BulletsPerShot;
    public ConsumableInventoryItemConfig ProjectileItemConfig;
}
}