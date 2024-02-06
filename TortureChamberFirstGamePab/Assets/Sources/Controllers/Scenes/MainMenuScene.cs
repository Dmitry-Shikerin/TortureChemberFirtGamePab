using System;
using Sources.Controllers.Forms.MainMenus;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Constants;
using Sources.Infrastructure.Factories.Controllers.Forms.MainMenus;
using Sources.Infrastructure.Factories.Services.Forms;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.Forms;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.Presentation.Views.Forms.Common;
using Sources.Presentation.Views.Forms.MainMenus;
using Sources.PresentationInterfaces.UI;

namespace Sources.Controllers.Scenes
{
    public class MainMenuScene : IScene
    {
        private readonly MainMenuHUD _mainMenuHUD;

        private readonly LeaderboardFormPresenterFactory _leaderboardFormPresenterFactory;
        private readonly MainMenuFormPresenterFactory _mainMenuFormPresenterFactory;
        private readonly YandexLeaderboardInitializeService _yandexLeaderboardInitializeService;
        private readonly FocusService _focusService;
        private readonly SDKInitializeService _sdkInitializeService;
        private readonly SceneService _sceneService;
        private readonly MainMenuFormServiceFactory _mainMenuFormServiceFactory;

        public MainMenuScene
        (
            LeaderboardFormPresenterFactory leaderboardFormPresenterFactory,
            MainMenuFormPresenterFactory mainMenuFormPresenterFactory,
            YandexLeaderboardInitializeService yandexLeaderboardInitializeService,
            FocusService focusService,
            SDKInitializeService sdkInitializeService,
            MainMenuHUD hud,
            SceneService sceneService,
            MainMenuFormServiceFactory mainMenuFormServiceFactory
        )
        {
            _mainMenuHUD = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _leaderboardFormPresenterFactory =
                leaderboardFormPresenterFactory ??
                throw new ArgumentNullException(nameof(leaderboardFormPresenterFactory));
            _mainMenuFormPresenterFactory =
                mainMenuFormPresenterFactory ??
                throw new ArgumentNullException(nameof(mainMenuFormPresenterFactory));
            _yandexLeaderboardInitializeService =
                yandexLeaderboardInitializeService ??
                throw new ArgumentNullException(nameof(yandexLeaderboardInitializeService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _sdkInitializeService = sdkInitializeService ??
                                    throw new ArgumentNullException(nameof(sdkInitializeService));
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
            _mainMenuFormServiceFactory = mainMenuFormServiceFactory ??
                                          throw new ArgumentNullException(nameof(mainMenuFormServiceFactory));
        }

        public string Name { get; } = nameof(MainMenuScene);

        public void Enter(object payload)
        {
            //TODO сделать остальные формочки по аналогии
            //TODO сделать MAinMenuFormServiceFactory и ретернуть IFormService
            //TODO создать его в фабрике мейнмену

            //TODO как то так
            _mainMenuFormServiceFactory
                .Create()
                .Show<MainMenuFormView>();
            
            _mainMenuHUD.Show();

            //TODO вроде здесь это должно быть
            // _sdkInitializeService.OnCallGameReadyButtonClick();
            // _focusService.Enter();
            // _yandexLeaderboardInitializeService.Fill();
        }

        public void Exit()
        {
            // _focusService.Exit();
        }

        public void Update(float deltaTime)
        {
        }

        public void UpdateLate(float deltaTime)
        {
        }


        public void UpdateFixed(float fixedDeltaTime)
        {
        }
    }
}