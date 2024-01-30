using System;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Factories.Views.UI;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.DIContainers
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuHUD _mainMenuHUD;
            
        public override void InstallBindings()
        {
            Container.Bind<MainMenuSceneFactory>().AsSingle();

            Container.Bind<MainMenuHUD>().FromInstance(_mainMenuHUD).AsSingle();

            Container.Bind<ButtonUIPresenterFactory>().AsSingle();
            Container.Bind<ButtonUIFactory>().AsSingle();
        }
    }
}