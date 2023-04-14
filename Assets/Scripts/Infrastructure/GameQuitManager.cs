using Core.Data.Handlers;
using UnityEngine;

internal class GameQuitManager : MonoBehaviour
{
    private GameDataManager _gameDataManager;


    public void Init(GameDataManager gameDataManager)
    {
        _gameDataManager = gameDataManager;
    }
    
    private void OnApplicationQuit()
    {
        _gameDataManager.Save();
    }
}
