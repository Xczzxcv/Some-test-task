using System.Collections.Generic;
using System.Linq;
using Core.Configs;

namespace Core.Data.Handlers
{
internal class DefaultGameDataInitializer : IGameDataInitializer
{
    private readonly IConfigsProvider _configsProvider;

    public DefaultGameDataInitializer(IConfigsProvider configsProvider)
    {
        _configsProvider = configsProvider;
    }

    public GameData InitialGameData()
    {
        return new GameData
        {
            Inventory = GetInventoryData(),
        };
    }

    private InventoryData GetInventoryData()
    {
        var weaponConfig = _configsProvider.InventoryItems.Values.FirstOrDefault(config => config is WeaponInventoryItemConfig);
        var consumableConfig = _configsProvider.InventoryItems.Values.FirstOrDefault(config => config is ConsumableInventoryItemConfig);
        var armorConfig = _configsProvider.InventoryItems.Values.FirstOrDefault(config => config is ArmorInventoryItemConfig);
        var inventoryData = new InventoryData
        {
            Slots = new List<InventorySlotData>
            {
                new()
                {
                    Item = new WeaponInventoryItemData
                    {
                        ItemId = weaponConfig.Id,
                        Amount = 1,
                    }
                },
                new()
                {
                    Item = new ConsumableInventoryItemData
                    {
                        ItemId = consumableConfig.Id,
                        Amount = 2
                    }
                },
                new()
                {
                    Item = null,
                },
                new()
                {
                    Item = new ArmorInventoryItemData
                    {
                        ItemId = armorConfig.Id,
                        Amount = 4,
                    }
                },
            }
        };

        while (inventoryData.Slots.Count < _configsProvider.Inventory.Size)
        {
            inventoryData.Slots.Add(new InventorySlotData {Item = null});
        }

        return inventoryData;
    }
}
}