﻿using System;
using Cysharp.Threading.Tasks;
using Sources.Controllers.Scenes;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.Payloads;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Factories.Scenes;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.InfrastructureInterfaces.Services.LoadServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;

namespace Sources.Infrastructure.Factories.Scenes
{
    public class GamePlaySceneFactory : ISceneFactory
    {
        private readonly IAdvertisingAfterCertainPeriodService _advertisingAfterCertainPeriodService;
        private readonly IBackgroundMusicService _backgroundMusicService;
        private readonly CreateService _createService;
        private readonly IFocusService _focusService;
        private readonly IGameOverService _gameOverService;
        private readonly IInputService _inputService;
        private readonly LoadService _loadService;
        private readonly ILocalizationService _localizationService;
        private readonly IMobilePlatformService _mobilePlatformService;
        private readonly PauseMenuService _pauseMenuService;
        private readonly ISaveAfterCertainPeriodService _saveAfterCertainPeriodService;
        private readonly TavernUpgradePointService _tavernUpgradePointService;
        private readonly IUpdateService _updateService;
        private readonly IQuantityService _visitorQuantityService;
        private readonly VisitorSpawnService _visitorSpawnService;
        private readonly IVolumeService _volumeService;

        public GamePlaySceneFactory(
            IMobilePlatformService mobilePlatformService,
            IVolumeService volumeService,
            IAdvertisingAfterCertainPeriodService advertisingAfterCertainPeriodService,
            ISaveAfterCertainPeriodService saveAfterCertainPeriodService,
            IGameOverService gameOverService,
            ILocalizationService localizationService,
            IFocusService focusService,
            IUpdateService updateService,
            IInputService inputService,
            VisitorSpawnService visitorSpawnService,
            TavernUpgradePointService tavernUpgradePointService,
            IQuantityService visitorQuantityService,
            PauseMenuService pauseMenuService,
            CreateService createService,
            LoadService loadService)
        {
            _mobilePlatformService =
                mobilePlatformService ?? throw new ArgumentNullException(nameof(mobilePlatformService));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
            _advertisingAfterCertainPeriodService =
                advertisingAfterCertainPeriodService ??
                throw new ArgumentNullException(nameof(advertisingAfterCertainPeriodService));
            _saveAfterCertainPeriodService = saveAfterCertainPeriodService ??
                                             throw new ArgumentNullException(nameof(saveAfterCertainPeriodService));
            _gameOverService = gameOverService ?? throw new ArgumentNullException(nameof(gameOverService));
            _localizationService = localizationService ??
                                   throw new ArgumentNullException(nameof(localizationService));
            _focusService = focusService;
            _inputService = inputService ?? throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _visitorSpawnService = visitorSpawnService ?? throw new ArgumentNullException(nameof(visitorSpawnService));
            _tavernUpgradePointService = tavernUpgradePointService ??
                                         throw new ArgumentNullException(nameof(tavernUpgradePointService));
            _visitorQuantityService =
                visitorQuantityService ?? throw new ArgumentNullException(nameof(visitorQuantityService));
            _pauseMenuService = pauseMenuService ?? throw new ArgumentNullException(nameof(pauseMenuService));
            _createService = createService ?? throw new ArgumentNullException(nameof(createService));
            _loadService = loadService ?? throw new ArgumentNullException(nameof(loadService));
        }

        public async UniTask<IScene> Create(object payload)
        {
            return new GamePlayScene(
                _mobilePlatformService,
                _volumeService,
                _advertisingAfterCertainPeriodService,
                _saveAfterCertainPeriodService,
                _gameOverService,
                _localizationService,
                _focusService,
                _inputService,
                _updateService,
                _visitorSpawnService,
                _tavernUpgradePointService,
                _visitorQuantityService,
                _pauseMenuService,
                CreateLoadService(payload));
        }

        private ILoadService CreateLoadService(object payload)
        {
            if (payload == null)
                return _createService;

            var canLoad = payload is LoadServicePayload { CanLoad: true };

            if (canLoad == false)
                return _createService;

            return _loadService;
        }
    }
}