namespace Core.Data.Handlers
{
internal interface ISaveDataHandler
{
    bool IsSaveDataExists();
    bool TryGetSaveData(out GameData gameData);
    void SaveData<T>(T data);
}
}