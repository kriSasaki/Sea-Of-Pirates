using Project.SDK.Advertisment;
using Project.SDK.InApp;
using Project.Systems.Audio;
using Project.Systems.Pause;
using System;
using UnityEngine;
using Zenject;

namespace Project.Installers.ProjectContext
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private AudioService _audioServicePrefab;

        public override void InstallBindings()
        {
            Container.Bind<AudioService>().FromComponentInNewPrefab(_audioServicePrefab).AsSingle();
            Container.Bind<PauseService>().FromNew().AsSingle();
            Container.Bind<AdvertismentController>().AsSingle();
            Container.Bind<AdvertismentService>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<FocusController>().FromNew().AsSingle().NonLazy();

            BindBillingProvider();
        }

        private void BindBillingProvider()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            Container.Bind<IBillingProvider>().To<BillingProvider>().FromNew().AsSingle();
#else
            Container.Bind<IBillingProvider>().To<MockBillingProvider>().FromNew().AsSingle();
#endif
        }
    }
}