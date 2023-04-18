using System;
using Core.Configs;
using Core.Data;
using Core.Models;
using UnityEngine;

namespace Infrastructure
{
internal class InventoryItemsFactory : IInventoryItemsFactory
{
    private readonly IConfigsProvider _configsProvider;

    public InventoryItemsFactory(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;
    }
    
    public IInventoryItem Create(InventoryItemData data)
    {
        if (!TryGetConfig(data.ItemId, out var config))
        {
            return InventoryItemHelper.CreateMockItem();
        }

        switch (config, data)
        {
            case (ArmorInventoryItemConfig armorConfig, ArmorInventoryItemData armorData):
                return new ArmorInventoryItem(armorConfig, armorData);
            case (ConsumableInventoryItemConfig consumableConfig, ConsumableInventoryItemData consumableData):
                return new ConsumableInventoryItem(consumableConfig, consumableData);
            case (WeaponInventoryItemConfig weaponConfig, WeaponInventoryItemData weaponData):
                return new WeaponInventoryItem(weaponConfig, weaponData);
            default:
                Debug.LogError("Config and/or data of item are not correct: " +
                               $"{config.Id}, {data.ItemId}");
                return InventoryItemHelper.CreateMockItem();
        }
    }

    public IInventoryItem Create(string itemId, int itemAmount)
    {
        if (!TryGetConfig(itemId, out var config))
        {
            return InventoryItemHelper.CreateMockItem();
        }

        var itemData = CreateItemData(config, itemAmount);
        return Create(itemData);
    }

    private InventoryItemData CreateItemData(InventoryItemConfig config, int itemAmount)
    {
        switch (config)
        {
            case ArmorInventoryItemConfig armorConfig:
                return new ArmorInventoryItemData(armorConfig.Id, itemAmount);
            case ConsumableInventoryItemConfig consumableConfig:
                return new ConsumableInventoryItemData(consumableConfig.Id, itemAmount);
            case WeaponInventoryItemConfig weaponConfig:
                return new WeaponInventoryItemData(weaponConfig.Id, itemAmount);
            default:
                throw new ArgumentException($"Config of item are not correct: {config.Id}");
        }
    }

    private bool TryGetConfig(string itemId, out InventoryItemConfig config)
    {
        if (!_configsProvider.InventoryItems.TryGetValue(itemId, out config))
        {
            Debug.LogError($"Can't get inventory item config by '{itemId}'");
            return false;
        }

        return true;
    }
}
}