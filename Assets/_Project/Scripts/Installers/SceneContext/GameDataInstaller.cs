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
            Container.Bind<IResourceStorageProvider>().To<ResourceStorageProvider>().AsSingle();
            Container.Bind<IPlayerStatsProvider>().To<PlayerStatsProvider>().AsSingle();
            Container.Bind<IQuestsProvider>().To<QuestsProvider>().AsSingle();

            Container.Bind<AdvertismentController>().AsSingle();
        }
    }
}