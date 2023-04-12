using Core.Configs;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
internal class ProjectInstaller : MonoInstaller
{
    [SerializeField] private ConfigsProvider configsProvider;

    public override void InstallBindings()
    {
        BindConfigsProvider();
    }

    private void BindConfigsProvider()
    {
        configsProvider.Init();
        Container.Bind<IConfigsProvider>().FromInstance(configsProvider).AsSingle();
    }
}
}