using System;

namespace Core.Data.Handlers
{
internal class GameDataManager : IGameDataProvider<InventoryData>
{
    private readonly ISaveDataHandler _saveDataHandler;
    private readonly IGameDataInitializer _gameDataInitializer;

    private GameData _gameData;

    public GameDataManager(ISaveDataHandler saveDataHandler,
        IGameDataInitializer gameDataInitializer)
    {
        _saveDataHandler = saveDataHandler;
        _gameDataInitializer = gameDataInitializer;
    }

    public void LoadData()
    {
        if (!_saveDataHandler.IsSaveDataExists())
        {
            _gameData = _gameDataInitializer.InitialGameData();
            return;
        }

        if (!_saveDataHandler.TryGetSaveData(out _gameData))
        {
            throw new AggregateException("Can't get saved data");
        }
    }

    public void Save()
    {
        _saveDataHandler.SaveData(_gameData);
    }

    InventoryData IGameDataProvider<InventoryData>.GetData() => _gameData.Inventory;
}
}
