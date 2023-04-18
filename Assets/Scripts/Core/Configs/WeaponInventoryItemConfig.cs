using Core.Models;
using UnityEngine;

namespace Core.Configs
{
[CreateAssetMenu(menuName = "Configs/Inventory Item/Weapon", fileName = "WeaponInventoryItemConfig", order = 0)]
internal class WeaponInventoryItemConfig : InventoryItemConfig
{
    public float DamageAmount;
    public int BulletsPerShot;
    public ConsumableInventoryItemConfig ProjectileItemConfig;

    private void OnValidate()
    {
        if (ProjectileItemConfig && 
            ProjectileItemConfig.ConsumableType != ConsumableItemType.Projectile)
        {
            Debug.LogError($"'{Id}': projectile ('{ProjectileItemConfig.Id}') " +
                           $"consumable item field '{nameof(ConsumableInventoryItemConfig.ConsumableType)}' " +
                           $"should be equal {nameof(ConsumableItemType.Projectile)}"
            );
        }
    }
}
}