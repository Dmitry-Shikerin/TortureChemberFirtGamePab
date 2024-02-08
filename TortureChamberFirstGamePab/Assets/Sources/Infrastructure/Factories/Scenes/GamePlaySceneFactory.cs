﻿using System;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.LoadServices.Payloads;
using Sources.Infrastructure.Services.SceneServices;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.Presentation.Voids;
using Zenject;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class GamePlaySceneFactory : ISceneFactory
    {
        private readonly HUD _hud;
        private readonly ILocalizationService _localizationService;
        private readonly IFocusService _focusService;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly SceneService _sceneService;
        private readonly IInputService _inputService;
        private readonly IUpdateService _updateService;
        private readonly VisitorSpawnService _visitorSpawnService;
        private readonly TavernUpgradePointService _tavernUpgradePointService;
        private readonly IQuantityService _visitorQuantityService;
        private readonly PauseMenuService _pauseMenuService;
        private readonly CreateService _createService;
        private readonly LoadService _loadService;

        public GamePlaySceneFactory
        (
            ILocalizationService localizationService,
            IFocusService focusService,
            HUD hud,
            ButtonUIFactory buttonUIFactory,
            IUpdateService updateService,
            SceneService sceneService,
            IInputService inputService,
            VisitorSpawnService visitorSpawnService,
            TavernUpgradePointService tavernUpgradePointService,
            IQuantityService visitorQuantityService,
            PauseMenuService pauseMenuService,
            CreateService createService,
            LoadService loadService
        )
        {
            _hud = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _localizationService = localizationService ??
                                   throw new ArgumentNullException(nameof(localizationService));
            _focusService = focusService;
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
            _sceneService = sceneService ??
                            throw new ArgumentNullException(nameof(sceneService));
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _visitorSpawnService = visitorSpawnService ?? throw new ArgumentNullException(nameof(visitorSpawnService));
            _tavernUpgradePointService = tavernUpgradePointService ?? throw new ArgumentNullException(nameof(tavernUpgradePointService));
            _visitorQuantityService = visitorQuantityService ?? throw new ArgumentNullException(nameof(visitorQuantityService));
            _pauseMenuService = pauseMenuService ?? throw new ArgumentNullException(nameof(pauseMenuService));
            _createService = createService ?? throw new ArgumentNullException(nameof(createService));
            _loadService = loadService ?? throw new ArgumentNullException(nameof(loadService));
        }

        public async UniTask<IScene> Create(object payload)
        {
            return new GamePlayScene
            (
                _localizationService,
                _focusService,
                _hud,
                _buttonUIFactory,
                _inputService,
                _updateService,
                _visitorSpawnService,
                _tavernUpgradePointService,
                _visitorQuantityService,
                _pauseMenuService,
                CreateLoadService(payload)
            );
        }

        private ILoadService CreateLoadService(object payload)
        {
            bool canLoad = payload is LoadServicePayload { CanLoad: true };
            
            if (canLoad == false)
                return _createService;

            return _loadService;
        }
    }
}