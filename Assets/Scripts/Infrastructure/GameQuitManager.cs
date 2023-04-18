using Core.Data.Handlers;
using UnityEngine;

namespace Infrastructure
{
internal class GameQuitManager : MonoBehaviour
{
    [SerializeField] private KeyCode exitKeyBinding;
    
    private GameDataManager _gameDataManager;

    public void Init(GameDataManager gameDataManager)
    {
        _gameDataManager = gameDataManager;
    }

    private void Update()
    {
        if (Input.GetKeyUp(exitKeyBinding))
        {
            Application.Quit();
        }
    }

    private void OnApplicationQuit()
    {
        _gameDataManager.Save();
    }
}
}
