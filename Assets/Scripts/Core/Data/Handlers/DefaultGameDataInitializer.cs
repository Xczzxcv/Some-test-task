using System.Collections.Generic;
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
        var inventorySize = _configsProvider.Inventory.Size;
        var initiallyUnlockedSlotsAmount = _configsProvider.Inventory.InitiallyUnlockedSlotsAmount;
        var inventoryData = new InventoryData
        {
            Slots = new List<InventorySlotData>(inventorySize)
        };

        AddEmptySlots(inventoryData, initiallyUnlockedSlotsAmount, true);
        AddEmptySlots(inventoryData, inventorySize- initiallyUnlockedSlotsAmount, false);

        return inventoryData;
    }

    private void AddEmptySlots(InventoryData inventoryData, int slotsAmount, bool isSlotsActive)
    {
        for (int i = 0; i < slotsAmount; i++)
        {
            var slotData = new InventorySlotData {Item = null, IsActive = isSlotsActive};
            inventoryData.Slots.Add(slotData);
        }
    }
}
}