using Project.Interfaces.Enemies;
using Project.Spawner;
using Project.Systems.Quests;
using Project.UI.Quests;
using Zenject;

namespace Project.Installers.SceneContext
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemySpawner>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesTo<EnemyDeathNotifier>().AsSingle();
            Container.Bind<QuestView>().FromComponentInHierarchy().AsSingle();

            Container.Bind<QuestGiver>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesTo<QuestSystem>().FromNew().AsSingle();
        }
    }
}