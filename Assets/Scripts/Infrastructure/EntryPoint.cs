using Core.Configs;
using Core.Data;
using Core.Data.Handlers;
using Core.Models;
using Infrastructure;
using UnityEngine;
using Zenject;

internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameQuitManager gameQuitManager;
    
    private ISaveDataHandler _saveDataHandler;
    private IGameDataInitializer _gameDataInitializer;
    private IInventoryItemFactory _inventoryItemFactory;
    private IConfigsProvider _configsProvider;

    [Inject]
    private void Construct(ISaveDataHandler saveDataHandler, 
        IGameDataInitializer gameDataInitializer,
        IInventoryItemFactory inventoryItemFactory,
        IConfigsProvider configsProvider)
    {
        _saveDataHandler = saveDataHandler;
        _gameDataInitializer = gameDataInitializer;
        _inventoryItemFactory = inventoryItemFactory;
        _configsProvider = configsProvider;
    }

    private void Start()
    {
        var gameDataManager = new GameDataManager(_saveDataHandler, _gameDataInitializer);
        gameDataManager.LoadData();
        
        gameQuitManager.Init(gameDataManager);

        TestSomething(gameDataManager);
    }

    private void TestSomething(IGameDataProvider<InventoryData> inventoryDataProvider)
    {
        var inventoryConfig = _configsProvider.Inventory;
        var inventoryData = inventoryDataProvider.GetData();
        IInventory inventory = new Inventory(inventoryConfig, inventoryData, _inventoryItemFactory);
        inventory.Init();
        for (var i = 0; i < inventory.Slots.Count; i++)
        {
            var inventorySlot = inventory.Slots[i];
            Debug.Log($"{i}: {inventorySlot.Item}");
        }
    }
}