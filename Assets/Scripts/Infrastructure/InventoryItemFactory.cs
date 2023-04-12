using System;
using Core.Configs;
using Core.Data;
using Core.Models;
using UnityEngine;

namespace Infrastructure
{
internal class InventoryItemFactory : IInventoryItemFactory
{
    private readonly IConfigsProvider _configsProvider;


    public InventoryItemFactory(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;
    }
    
    public IInventoryItem Create(InventoryItemData data)
    {
        if (!_configsProvider.InventoryItems.TryGetValue(data.ItemId, out var config))
        {
            Debug.LogError($"Can't get inventory item config by '{data.ItemId}'");
            return InventoryItemHelper.CreateMockItem();
        }

        return config switch
        {
            
            _ => throw new ArgumentOutOfRangeException(nameof(config))
        };
    }
}
}