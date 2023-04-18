using Ui;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
internal class UiFactory : MonoBehaviour
{
    [SerializeField] private MainScreenController mainScreenControllerPrefab;
    [SerializeField] private RectTransform uiRoot;

    private DiContainer _container;

    public void Init(DiContainer diContainer)
    {
        _container = diContainer;
    }

    public MainScreenController CreateMainScreenController()
    {
        return _container
            .InstantiatePrefabForComponent<MainScreenController>(mainScreenControllerPrefab, uiRoot);
    }
}
}