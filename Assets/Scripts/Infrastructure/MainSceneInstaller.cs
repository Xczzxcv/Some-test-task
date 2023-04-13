using Core.Data.Handlers;
using Zenject;

namespace Infrastructure
{
internal class MainSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindSaveDataHandler();
        BindDataInitializerHandler();
    }

    private void BindSaveDataHandler()
    {
        Container.Bind<ISaveDataHandler>().To<LocalJsonSaveDataHandler>().AsSingle();
    }

    private void BindDataInitializerHandler()
    {
        Container.Bind<IGameDataInitializer>().To<DefaultGameDataInitializer>().AsSingle();
    }
}
}