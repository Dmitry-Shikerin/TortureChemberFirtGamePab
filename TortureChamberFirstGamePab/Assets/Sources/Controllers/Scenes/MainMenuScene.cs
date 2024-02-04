using System;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Constants;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.PresentationInterfaces.UI;

namespace Sources.Controllers.Scenes
{
    public class MainMenuScene : IScene
    {
        private readonly MainMenuHUD _mainMenuHUD;

        private readonly YandexLeaderboardInitializeService _yandexLeaderboardInitializeService;
        private readonly FocusService _focusService;
        private readonly SDKInitializeService _sdkInitializeService;
        private readonly IDataService<Domain.Players.Data.Player> _dataService;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly SceneService _sceneService;

        public MainMenuScene
        (
            YandexLeaderboardInitializeService yandexLeaderboardInitializeService,
            FocusService focusService,
            SDKInitializeService sdkInitializeService,
            MainMenuHUD hud,
            IDataService<Domain.Players.Data.Player> dataService,
            ButtonUIFactory buttonUIFactory,
            SceneService sceneService
        )
        {
            _mainMenuHUD = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _yandexLeaderboardInitializeService = 
                yandexLeaderboardInitializeService ?? 
                throw new ArgumentNullException(nameof(yandexLeaderboardInitializeService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _sdkInitializeService = sdkInitializeService ??
                                    throw new ArgumentNullException(nameof(sdkInitializeService));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
        }

        public string Name { get; } = nameof(MainMenuScene);

        public void Enter(object payload)
        {
            IButtonUI continueGameButton = _buttonUIFactory.Create(
                _mainMenuHUD.ButtonUIContainer.ContinueGameButton, async () =>
                    await _sceneService.ChangeSceneAsync(Constant.SceneNames.Gameplay,
                        new LoadServicePayload(true)));

            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.NewGameButton, async () =>
                await _sceneService.ChangeSceneAsync(Constant.SceneNames.Gameplay,
                    new LoadServicePayload(false)));

            if (_dataService.CanLoad == false)
                continueGameButton.Disable();

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