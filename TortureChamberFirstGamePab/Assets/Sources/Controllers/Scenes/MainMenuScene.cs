using System;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Constants;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.PresentationInterfaces.UI;

namespace Sources.Controllers.Scenes
{
    public class MainMenuScene : IScene
    {
        private readonly MainMenuHUD _mainMenuHUD;

        private readonly IDataService<Domain.Players.Data.Player> _dataService;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly SceneService _sceneService;

        public MainMenuScene
        (
            MainMenuHUD hud,
            IDataService<Domain.Players.Data.Player> dataService,
            ButtonUIFactory buttonUIFactory, SceneService sceneService
        )
        {
            _mainMenuHUD = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _sceneService = sceneService ?? throw new ArgumentNullException(nameof(sceneService));
        }

        public string Name { get; } = nameof(MainMenuScene);

        public void Enter(object payload)
        {
            IButtonUI continueGameButton = _buttonUIFactory.Create(
                _mainMenuHUD.ButtonUIContainer.ContinueGameButton, async () =>
                    await _sceneService.ChangeSceneAsync(Constant.SceneNames.GamePlay,
                        new LoadServicePayload(true)));

            _buttonUIFactory.Create(_mainMenuHUD.ButtonUIContainer.NewGameButton, async () =>
                await _sceneService.ChangeSceneAsync(Constant.SceneNames.GamePlay,
                    new LoadServicePayload(false)));

            if (_dataService.CanLoad == false)
                continueGameButton.Disable();

            _mainMenuHUD.Show();
        }

        public void Exit()
        {
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