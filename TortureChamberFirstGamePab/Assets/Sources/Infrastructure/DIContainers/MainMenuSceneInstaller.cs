using Lean.Localization;
using Sources.Infrastructure.Factories.Controllers.Forms;
using Sources.Infrastructure.Factories.Controllers.Forms.MainMenus;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Controllers.YandexSDC;
using Sources.Infrastructure.Factories.Scenes;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Factories.Views.YandexSDC;
using Sources.Infrastructure.Services.Forms;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.Presentation.Views;
using Sources.Presentation.Views.YandexSDC.MyVariant;
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