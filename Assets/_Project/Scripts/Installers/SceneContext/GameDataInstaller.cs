using Project.Interfaces.Data;
using Project.SDK.Advertisment;
using Project.Systems.Data;
using Zenject;

namespace Project.Installers.SceneContext
{
    public class GameDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameDataService>().AsSingle().NonLazy();
            Container.Bind<IResourceStorageProvider>().To<ResourceStorageProvider>().AsSingle().NonLazy();
            Container.Bind<IPlayerStatsProvider>().To<PlayerStatsProvider>().AsSingle().NonLazy();
            Container.Bind<IQuestsProvider>().To<QuestsProvider>().AsSingle().NonLazy();

            Container.Bind<AdvertismentController>().AsSingle().NonLazy();
        }
    }
}