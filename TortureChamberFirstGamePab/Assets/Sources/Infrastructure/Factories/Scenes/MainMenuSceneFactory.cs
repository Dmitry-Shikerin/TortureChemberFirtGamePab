using System;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Players.Data;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class MainMenuSceneFactory : ISceneFactory
    {
        private readonly YandexLeaderboardInitializeService _yandexLeaderboardInitializeService;
        private readonly FocusService _focusService;
        private readonly SDKInitializeService _sdkInitializeService;
        private readonly SceneService _sceneService;
        private readonly IDataService<Player> _dataService;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly MainMenuHUD _mainMenuHUD;

        private bool _canLoad;

        public MainMenuSceneFactory
        (
            YandexLeaderboardInitializeService yandexLeaderboardInitializeService,
            FocusService focusService,
            SDKInitializeService sdkInitializeService,
            SceneService sceneService,
            IDataService<Player> dataService,
            MainMenuHUD mainMenuHUD,
            ButtonUIFactory buttonUIFactory
        )
        {
            _yandexLeaderboardInitializeService =
                yandexLeaderboardInitializeService ??
                throw new ArgumentNullException(nameof(yandexLeaderboardInitializeService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _sdkInitializeService = sdkInitializeService ?? 
                                    throw new ArgumentNullException(nameof(sdkInitializeService));
            _sceneService = sceneService ??
                            throw new ArgumentNullException(nameof(sceneService));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _mainMenuHUD = mainMenuHUD ? mainMenuHUD : throw new ArgumentNullException(nameof(mainMenuHUD));
        }

        public async UniTask<IScene> Create(object payload)
        {
            return new MainMenuScene
            (
                _yandexLeaderboardInitializeService,
                _focusService,
                _sdkInitializeService,
                _mainMenuHUD,
                _dataService,
                _buttonUIFactory,
                _sceneService
            );
        }
    }
}