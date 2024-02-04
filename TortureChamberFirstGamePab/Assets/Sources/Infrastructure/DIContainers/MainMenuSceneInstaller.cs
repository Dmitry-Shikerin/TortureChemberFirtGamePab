using System;
using Agava.YandexGames;
using Lean.Localization;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Controllers.YandexSDC;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.YandexSDC;
using Sources.Infrastructure.Services.YandexSDCServices;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.DIContainers
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuHUD _mainMenuHUD;
        [SerializeField] private LeanLocalization _leanLocalization;
            
        public override void InstallBindings()
        {
            Container.Bind<MainMenuSceneFactory>().AsSingle();

            Container.Bind<MainMenuHUD>().FromInstance(_mainMenuHUD).AsSingle();

            Container.Bind<ButtonUIPresenterFactory>().AsSingle();
            Container.Bind<ButtonUIFactory>().AsSingle();
            
            //TODO здесь ли регестрировать?
            
            Container.Bind<LeaderboardElementPresenterFactory>().AsSingle();
            Container.Bind<LeaderboardElementViewFactory>().AsSingle();

            Container.Bind<YandexLeaderboardInitializeService>().AsSingle();
            //TODO придется регать так на каждой сцене потомучто постоянно разные экземпляры LeanLocalization
            Container.Bind<LeanLocalization>().FromInstance(_leanLocalization).AsSingle();
            Container.Bind<LocalizationService>().AsSingle();
        }
    }
}