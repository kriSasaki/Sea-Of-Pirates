using Project.Players.Inputs;
using Project.Systems.Data;
using Project.Systems.Storage;
using UnityEngine;
using YG;
using Zenject;

namespace Project.Installers.ProjectContext
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