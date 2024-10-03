using Project.Configs.Game;
using Project.Configs.GameResources;
using Project.Configs.ShopItems;
using Project.Configs.UI;
using UnityEngine;
using Zenject;

namespace Project.Installers.ProjectContext
{
    public class GameSettingsInstaller : MonoInstaller
    {
        [SerializeField] private GameResourcesSheet _resourcesSheet;
        [SerializeField] private ShopItemsConfigs _shopItemsCongigs;
        [SerializeField] private StatsSheet _statsSheet;
        [SerializeField] private UiConfigs _uiConfigs;
        [SerializeField] private GameConfig _gameConfig;

        public override void InstallBindings()
        {
            Container.Bind<GameResourcesSheet>().FromInstance(_resourcesSheet);
            Container.Bind<StatsSheet>().FromInstance(_statsSheet);
            Container.Bind<ShopItemsConfigs>().FromInstance(_shopItemsCongigs);
            Container.Bind<UiConfigs>().FromInstance(_uiConfigs);
            Container.Bind<GameConfig>().FromInstance(_gameConfig);
        }
    }
}