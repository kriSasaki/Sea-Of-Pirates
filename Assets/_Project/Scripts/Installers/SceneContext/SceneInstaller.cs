using Project.Interfaces.Enemies;
using Project.Spawner;
using Project.Systems.Quests;
using Project.UI.Quests;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Installers.SceneContext
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Button _questButton;

        public override void InstallBindings()
        {
            BindEnemies();
            BindUI();
            BindQuestSystem();
        }

        private void BindEnemies()
        {
            Container.Bind<EnemySpawner>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesTo<EnemyDeathNotifier>().AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<QuestWindow>().FromComponentInHierarchy().AsSingle();
            Container.Bind<QuestView>().AsSingle().WithArguments(_questButton);
        }

        private void BindQuestSystem()
        {
            Container.Bind<QuestGiver>().FromComponentsInHierarchy().AsCached();
            Container.BindInterfacesTo<QuestSystem>().FromNew().AsSingle();
        }
    }
}