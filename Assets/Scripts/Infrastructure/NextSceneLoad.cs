using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
public class NextSceneLoad : MonoBehaviour
{
    [SerializeField] private string loadedSceneName;
    
    private void Awake()
    {
        SceneManager.LoadScene(loadedSceneName);
    }
}
}
