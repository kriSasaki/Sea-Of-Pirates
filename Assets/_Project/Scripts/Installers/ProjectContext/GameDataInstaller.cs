using Scripts.Interfaces.Data;
using Scripts.Systems.Data;
using Zenject;

namespace Scripts.Installers.ProjectContext
{
    public class GameDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameDataService>().AsSingle().NonLazy();
            Container.Bind<IResourceStorageProvider>().To<ResourceStorageProvider>().AsSingle().NonLazy();
            Container.Bind<IPlayerStatsProvider>().To<PlayerStatsProvider>().AsSingle().NonLazy();
            Container.Bind<IQuestsProvider>().To<QuestsProvider>().AsSingle().NonLazy();
        }
    }
}