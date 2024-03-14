using System;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.InfrastructureInterfaces.Services.LoadServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;

namespace Sources.Controllers.Scenes
{
    public class GamePlayScene : IScene
    {
        private readonly IAdvertisingAfterCertainPeriodService _advertisingAfterCertainPeriodService;
        private readonly IBackgroundMusicService _backgroundMusicService;
        private readonly IFocusService _focusService;
        private readonly IGameOverService _gameOverService;
        private readonly IInputService _inputService;
        private readonly ILoadService _loadService;
        private readonly ILocalizationService _localizationService;
        private readonly IMobilePlatformService _mobilePlatformService;
        private readonly PauseMenuService _pauseMenuService;
        private readonly ISaveAfterCertainPeriodService _saveAfterCertainPeriodService;
        private readonly TavernUpgradePointService _tavernUpgradePointService;
        private readonly IUpdateService _updateService;
        private readonly IQuantityService _visitorQuantityService;
        private readonly VisitorSpawnService _visitorSpawnService;
        private readonly IVolumeService _volumeService;

        public GamePlayScene(
            IMobilePlatformService mobilePlatformService,
            IVolumeService volumeService,
            IAdvertisingAfterCertainPeriodService advertisingAfterCertainPeriodService,
            ISaveAfterCertainPeriodService saveAfterCertainPeriodService,
            IGameOverService gameOverService,
            ILocalizationService localizationService,
            IFocusService focusService,
            IInputService inputService,
            IUpdateService updateService,
            VisitorSpawnService visitorSpawnService,
            TavernUpgradePointService tavernUpgradePointService,
            IQuantityService visitorQuantityService,
            PauseMenuService pauseMenuService,
            ILoadService loadService)
        {
            _mobilePlatformService = mobilePlatformService ??
                                     throw new ArgumentNullException(nameof(mobilePlatformService));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
            _advertisingAfterCertainPeriodService =
                advertisingAfterCertainPeriodService ??
                throw new ArgumentNullException(nameof(advertisingAfterCertainPeriodService));
            _saveAfterCertainPeriodService = saveAfterCertainPeriodService ??
                                             throw new ArgumentNullException(nameof(saveAfterCertainPeriodService));
            _gameOverService = gameOverService ?? throw new ArgumentNullException(nameof(gameOverService));
            _localizationService = localizationService ??
                                   throw new ArgumentNullException(nameof(localizationService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _inputService = inputService ??
                            throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _visitorSpawnService = visitorSpawnService ?? throw new ArgumentNullException(nameof(visitorSpawnService));
            _tavernUpgradePointService = tavernUpgradePointService ??
                                         throw new ArgumentNullException(nameof(tavernUpgradePointService));
            _visitorQuantityService =
                visitorQuantityService ?? throw new ArgumentNullException(nameof(visitorQuantityService));
            _pauseMenuService = pauseMenuService ?? throw new ArgumentNullException(nameof(pauseMenuService));
            _loadService = loadService ?? throw new ArgumentNullException(nameof(loadService));
        }

        public string Name { get; } = nameof(GamePlayScene);

        public void Enter(object payload)
        {
            _loadService.Load();
            _tavernUpgradePointService.OnEnable();
            _visitorQuantityService.Enter();
            _visitorSpawnService.Enter();
            _pauseMenuService.Enter();
            _gameOverService.Enter();
            _saveAfterCertainPeriodService.Enter(_loadService);
            _advertisingAfterCertainPeriodService.Enter();
            _volumeService.Enter();
            _mobilePlatformService.Enter();

            _localizationService.Enter();
            _focusService.Enter();
        }

        public void Exit()
        {
            _tavernUpgradePointService.OnDisable();
            _visitorQuantityService.Exit();
            _visitorSpawnService.Exit();
            _pauseMenuService.Exit();
            _gameOverService.Exit();
            _saveAfterCertainPeriodService.Exit();
            _advertisingAfterCertainPeriodService.Exit();
            _volumeService.Exit();

            _focusService.Exit();
        }

        public void Update(float deltaTime)
        {
            _inputService.Update(deltaTime);
            _updateService.Update(deltaTime);
        }

        public void UpdateLate(float deltaTime)
        {
            _inputService.UpdateLate(deltaTime);
            _updateService.UpdateLate(deltaTime);
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
            _inputService.UpdateFixed(fixedDeltaTime);
            _updateService.UpdateFixed(fixedDeltaTime);
        }
    }
}