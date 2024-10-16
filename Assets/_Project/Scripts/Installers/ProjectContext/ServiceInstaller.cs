using Lean.Localization;
using Scripts.Interfaces.SDK;
using Scripts.SDK;
using Scripts.SDK.Advertisment;
using Scripts.SDK.InApp;
using Scripts.SDK.Leaderboard;
using UnityEngine;
using Zenject;

namespace Scripts.Installers.ProjectContext
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private LeanLocalization _localizationPrefab;

        public override void InstallBindings()
        {
            Container.Bind<LeanLocalization>().FromComponentInNewPrefab(_localizationPrefab).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<BroAudioService>().AsSingle();

            BindSDK();
        }

        private void BindSDK()
        {
            Container.Bind<ILeaderboardService>().To<YandexLeaderboardService>().FromNew().AsSingle();
            Container.Bind<IAdvertismentService>().To<YandexAdvertismentService>().FromNew().AsSingle();
            Container.Bind<IGameReadyService>().To<GameReadyService>().FromNew().AsSingle();
            Container.Bind<IBillingService>().To<YandexBillingService>().FromNew().AsSingle();
            Container.Bind<IMetricaService>().To<MetricaService>().FromNew().AsSingle();
        }
    }
}