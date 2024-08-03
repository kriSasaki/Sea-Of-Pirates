using Project.Players.Inputs;
using Project.Players.PlayerLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        if (Agava.WebUtility.Device.IsMobile)
        {
            Container.Bind<IInputService>().To<MobileInputService>().FromNew().AsSingle();
        }
        else
        {
            Container.Bind<IInputService>().To<StandaloneInputService>().FromNew().AsSingle();
        }
     
        Container.Bind<PlayerMove>().FromComponentInHierarchy().AsTransient();
    }
}