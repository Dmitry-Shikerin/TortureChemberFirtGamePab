using Infrastructure.Factories.Controllers.Forms;
using Infrastructure.Factories.Controllers.Forms.MainMenus;
using Infrastructure.Factories.Controllers.UI;
using Infrastructure.Factories.Controllers.YandexSDC;
using Infrastructure.Factories.Scenes;
using Infrastructure.Factories.Services.Forms;
using Infrastructure.Factories.Views.UI;
using Infrastructure.Factories.Views.YandexSDC;
using Infrastructure.Services.Forms;
using Infrastructure.Services.YandexSDCServices;
using InfrastructureInterfaces.Services.SDCServices;
using Lean.Localization;
using Presentation.Containers.HUDs;
using Presentation.Views;
using Presentation.Views.YandexSDC.MyVariant;
using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainers
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

            Container.Bind<ContainerView>().FromInstance(_mainMenuHUD.ContainerView).AsSingle();
            Container.BindInterfacesAndSelfTo<FormService>().AsSingle();

            Container.Bind<MainMenuFormPresenterFactory>().AsSingle();
            Container.Bind<LeaderboardFormPresenterFactory>().AsSingle();
            Container.Bind<SettingFormPresenterFactory>().AsSingle();
            Container.Bind<AuthorizationFormPresenterFactory>().AsSingle();
            Container.Bind<NewGameFormPresenterFactory>().AsSingle();

            Container.Bind<LeaderboardElementPresenterFactory>().AsSingle();
            Container.Bind<LeaderboardElementViewFactory>().AsSingle();

            Container.Bind<LeaderboardElementViewContainer>()
                .FromInstance(_mainMenuHUD.LeaderboardElementViewContainer).AsSingle();
            Container.Bind<ILeaderboardInitializeService>().To<YandexLeaderboardInitializeService>().AsSingle();
            Container.Bind<LeanLocalization>().FromInstance(_mainMenuHUD.LeanLocalization).AsSingle();
            Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle();
            Container.Bind<IPlayerAccountAuthorizeService>().To<PlayerAccountAuthorizeService>().AsSingle();

            Container.Bind<MainMenuFormServiceFactory>().AsSingle();
        }
    }
}