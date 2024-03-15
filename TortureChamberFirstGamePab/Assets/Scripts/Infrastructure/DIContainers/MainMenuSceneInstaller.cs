using Lean.Localization;
using Scripts.Infrastructure.Factories.Controllers.Forms;
using Scripts.Infrastructure.Factories.Controllers.Forms.MainMenus;
using Scripts.Infrastructure.Factories.Controllers.UI;
using Scripts.Infrastructure.Factories.Controllers.YandexSDC;
using Scripts.Infrastructure.Factories.Scenes;
using Scripts.Infrastructure.Factories.Services.Forms;
using Scripts.Infrastructure.Factories.Views.UI;
using Scripts.Infrastructure.Factories.Views.YandexSDC;
using Scripts.Infrastructure.Services.Forms;
using Scripts.Infrastructure.Services.YandexSDCServices;
using Scripts.InfrastructureInterfaces.Services.SDCServices;
using Scripts.Presentation.Containers.HUDs;
using Scripts.Presentation.Views;
using Scripts.Presentation.Views.YandexSDC.MyVariant;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.DIContainers
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