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
            Container.Bind<EnemySpawner>().FromComponentsInHierarchy();
            Container.Bind<IEnemyDeathNotifier>().FromComponentInHierarchy().AsSingle();
            Container.Bind<QuestView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<QuestSystem>().FromNew().AsSingle();

            Container.Bind<QuestGiver>().FromComponentsInHierarchy();
        }
    }
}