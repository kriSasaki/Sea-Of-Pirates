using Scripts.Players.Inputs;
using Scripts.Systems.Data;
using Scripts.Systems.Storage;
using UnityEngine;
using YG;
using Zenject;

namespace Scripts.Installers.ProjectContext
{
    public class GameSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
        }

        private void BindInput()
        {
            Container.Bind<IInputService>().To<InputService>().FromNew().AsSingle();
        }
    }
}