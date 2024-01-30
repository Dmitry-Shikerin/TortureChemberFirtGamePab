using System;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Constants;
using Sources.Domain.Players;
using Sources.Domain.Players.Data;
using Sources.Infrastructure.Factories.Controllers.UI;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.Presentation.UI.Conteiners.MainMenu;
using Sources.Presentation.Voids;
using Sources.PresentationInterfaces.UI;
using Object = UnityEngine.Object;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class MainMenuSceneFactory : ISceneFactory
    {
        private readonly SceneService _sceneService;
        private readonly IDataService<Player> _dataService;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly MainMenuHUD _mainMenuHUD;

        private bool _canLoad;

        public MainMenuSceneFactory
        (
            SceneService sceneService,
            IDataService<Player> dataService,
            MainMenuHUD mainMenuHUD,
            ButtonUIFactory buttonUIFactory
        )
        {
            _sceneService = sceneService ??
                            throw new ArgumentNullException(nameof(sceneService));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _mainMenuHUD = mainMenuHUD ? mainMenuHUD : throw new ArgumentNullException(nameof(mainMenuHUD));
        }

        public async UniTask<IScene> Create(object payload)
        {
            //MainMenuButtons
            // HudButtonUIContainer hudButtonUIContainer = _mainMenuHUD.ButtonUIContainer;
            //
            // IButtonUI continueGameButton = _buttonUIFactory.Create(
            //     hudButtonUIContainer.ContinueGameButton, LoadGamePlayScene);
            //
            // _buttonUIFactory.Create(hudButtonUIContainer.NewGameButton, CreateGamePlayScene);
            // buttonUIFactory.Create(hudButtonUIContainer.OptionsButton,);

            return new MainMenuScene
            (
                _mainMenuHUD,
                // continueGameButton,
                _dataService,
                _buttonUIFactory,
                _sceneService
            );
        }

        private async void LoadGamePlayScene() =>
            await _sceneService.ChangeSceneAsync(Constant.SceneNames.GamePlay, new LoadServicePayload(true));

        private async void CreateGamePlayScene()
        {
            _dataService.Clear();

            await _sceneService.ChangeSceneAsync(Constant.SceneNames.GamePlay, new LoadServicePayload(false));
        }
    }
}