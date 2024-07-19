using Project.Configs.GameResources;
using Project.Interfaces.Data;
using Project.Systems.Data;
using UnityEngine;
using Zenject;

namespace Project.Installers
{
    public class GameDataInstaller : MonoInstaller
    {
        [SerializeField] private GameResourcesSheet _resourcesSheet;

        public override void InstallBindings()
        {
            Container.Bind<GameResourcesSheet>().FromInstance(_resourcesSheet);

            Container.BindInterfacesAndSelfTo<GameDataService>().AsSingle().NonLazy();

            Container.Bind<IResourceStorageProvider>().To<ResourceStorageProvider>().AsSingle();
        }
    }
}