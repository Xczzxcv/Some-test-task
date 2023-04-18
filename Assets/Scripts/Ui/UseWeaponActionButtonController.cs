using System.Collections.Generic;
using Core.Models;
using UnityEngine;

namespace Ui
{
internal class UseWeaponActionButtonController : ActionButtonController
{
    [SerializeField] private int projectilesCost;

    protected override void PerformAction()
    {
        if (!TryFindProjectileConsumables(out var slotsWithProjectiles))
        {
            Debug.LogError("No projectiles in inventory");
            return;
        }

        var randomProjectileSlot = ChooseRandomSlot(slotsWithProjectiles);
        DecreaseProjectileAmount(randomProjectileSlot);
    }

    private bool TryFindProjectileConsumables(out List<IInventorySlot> projectileSlots)
    {
        projectileSlots = new List<IInventorySlot>();
        foreach (var slot in Inventory.Slots)
        {
            if (slot.Item is ConsumableInventoryItem {ConsumableType: ConsumableItemType.Projectile} 
                && slot.Item.Amount >= projectilesCost)
            {
                projectileSlots.Add(slot);
            }
        }

        return projectileSlots.Count > 0;
    }

    private IInventorySlot ChooseRandomSlot(IReadOnlyList<IInventorySlot> slotsWithProjectiles)
    {
        return slotsWithProjectiles[Random.Range(0, slotsWithProjectiles.Count)];
    }

    private void DecreaseProjectileAmount(IInventorySlot randomProjectileSlot)
    {
        if (!randomProjectileSlot.TryTakeAway(projectilesCost, out _))
        {
            Debug.LogError($"Can't take away projectile from slot {randomProjectileSlot}");
        }
    }

    private bool TryUseWeapon(WeaponInventoryItem weapon, IInventory consumablesInventory)
    {
        if (!consumablesInventory.TryTakeAway(weapon.ProjectileItemId, weapon.ProjectilesPerShot,
                out var projectiles))
        {
            Debug.Log($"Can't use weapon '{weapon.Id}' (too few '{projectiles.Id}' in inventory)");
            return false;
        }

        Debug.Log($"Weapon '{weapon.Id}' was used. {weapon.ProjectilesPerShot} projectiles " +
                  $"'{projectiles.Id}' were taken away from inventory");
        return true;
    }
}
}