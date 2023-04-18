using Core.Data.Handlers;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
internal class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private UiFactory uiFactory;
    [SerializeField] private InventorySlotControllersFactory inventorySlotControllersFactory;
    
    public override void InstallBindings()
    {
        BindSaveDataHandler();
        BindDataInitializerHandler();
        InitUiFactory();
        BindInventorySlotFactory();
    }

    private void BindSaveDataHandler()
    {
        Container.Bind<ISaveDataHandler>().To<LocalJsonSaveDataHandler>().AsSingle();
    }

    private void BindDataInitializerHandler()
    {
        Container.Bind<IGameDataInitializer>().To<DefaultGameDataInitializer>().AsSingle();
    }

    private void InitUiFactory()
    {
        uiFactory.Init(Container);
    }

    private void BindInventorySlotFactory()
    {
        inventorySlotControllersFactory.Init(Container);
        Container.Bind<IInventorySlotControllersFactory>().FromInstance(inventorySlotControllersFactory);
    }
}
}