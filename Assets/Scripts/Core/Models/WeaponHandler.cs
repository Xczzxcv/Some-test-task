using UnityEngine;

namespace Core.Models
{
internal class WeaponHandler
{
    public bool CanUseWeapon(WeaponInventoryItem weapon, IInventory consumablesInventory)
    {
        return consumablesInventory.GetAmount(weapon.ProjectileItemId) >= weapon.ProjectilesPerShot;
    }
    
    public bool TryUseWeapon(WeaponInventoryItem weapon, IInventory consumablesInventory)
    {
        if (!consumablesInventory.TryTakeAway(weapon.ProjectileItemId, weapon.ProjectilesPerShot,
                out var projectiles))
        {
            Debug.Log($"Can't use weapon '{weapon.Id}' (too few {projectiles.Id} in inventory)");
            return false;
        }

        Debug.Log($"Weapon '{weapon.Id}' was used. {weapon.ProjectilesPerShot} projectiles " +
                  $"'{projectiles.Id}' were taken away from inventory");
        return true;
    }
}
}