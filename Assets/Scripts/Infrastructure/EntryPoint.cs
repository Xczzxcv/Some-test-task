using Core.Configs;
using Core.Data;
using Core.Data.Handlers;
using Core.Models;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
internal class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameQuitManager gameQuitManager;
    [SerializeField] private UiFactory uiFactory;
    
    private ISaveDataHandler _saveDataHandler;
    private IGameDataInitializer _gameDataInitializer;
    private IInventoryItemsFactory _inventoryItemsFactory;
    private IConfigsProvider _configsProvider;

    [Inject]
    private void Construct(ISaveDataHandler saveDataHandler, 
        IGameDataInitializer gameDataInitializer,
        IInventoryItemsFactory inventoryItemsFactory,
        IConfigsProvider configsProvider)
    {
        _saveDataHandler = saveDataHandler;
        _gameDataInitializer = gameDataInitializer;
        _inventoryItemsFactory = inventoryItemsFactory;
        _configsProvider = configsProvider;
    }

    private void Start()
    {
        var gameDataManager = new GameDataManager(_saveDataHandler, _gameDataInitializer);
        gameDataManager.LoadData();
        
        gameQuitManager.Init(gameDataManager);
        
        var inventory = GetTestInventory(gameDataManager);
        var mainScreenController = uiFactory.CreateMainScreenController();
        mainScreenController.Setup(inventory);
    }

    private IInventory GetTestInventory(IGameDataProvider<InventoryData> inventoryDataProvider)
    {
        var inventoryConfig = _configsProvider.Inventory;
        var inventoryData = inventoryDataProvider.GetData();
        IInventory inventory = new Inventory(inventoryConfig, inventoryData, _inventoryItemsFactory);
        inventory.Init();

        return inventory;
    }
}
}