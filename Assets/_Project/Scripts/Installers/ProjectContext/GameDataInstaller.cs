using Project.Configs.GameResources;
using Project.Configs.ShopItems;
using Project.Interfaces.Data;
using Project.SDK.Advertisment;
using Project.Systems.Data;
using Project.Systems.Stats;
using Project.Systems.Storage;
using UnityEngine;
using Zenject;

namespace Project.Installers.ProjectContext
{
    public class GameDataInstaller : MonoInstaller
    {
        [SerializeField] private GameResourcesSheet _resourcesSheet;
        [SerializeField] private ShopItemsConfigs _shopItemsSheet;
        [SerializeField] private StatsSheet _statsSheet;

        public override void InstallBindings()
        {
            Container.Bind<GameResourcesSheet>().FromInstance(_resourcesSheet);
            Container.Bind<StatsSheet>().FromInstance(_statsSheet);
            Container.Bind<ShopItemsConfigs>().FromInstance(_shopItemsSheet);

            Container.BindInterfacesTo<GameDataService>().AsSingle().NonLazy();

            Container.Bind<IResourceStorageProvider>().To<ResourceStorageProvider>().AsSingle();
            Container.Bind<IPlayerStatsProvider>().To<PlayerStatsProvider>().AsSingle();
            Container.Bind<IQuestsProvider>().To<QuestsProvider>().AsSingle();

            Container.Bind<AdvertismentController>().AsSingle();
            Container.BindInterfacesTo<PlayerStats>().AsSingle();
            Container.BindInterfacesTo<PlayerStorage>().AsSingle();
        }
    }
}