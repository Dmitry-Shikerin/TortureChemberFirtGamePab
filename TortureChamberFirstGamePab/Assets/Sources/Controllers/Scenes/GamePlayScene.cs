using System;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.SDCServices.WebGlServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.Presentation.Voids;

namespace Sources.Controllers.Scenes
{
    public class GamePlayScene : IScene
    {
        private readonly HUD _hud;
        private readonly IVolumeService _volumeService;
        private readonly IAdvertisingAfterCertainPeriodService _advertisingAfterCertainPeriodService;
        private readonly ISaveAfterCertainPeriodService _saveAfterCertainPeriodService;
        private readonly IGameOverService _gameOverService;
        private readonly IBackgroundMusicService _backgroundMusicService;
        private readonly ILocalizationService _localizationService;
        private readonly IFocusService _focusService;
        private readonly ButtonUIFactory _buttonUIFactory;
        private readonly IInputService _inputService;
        private readonly IUpdateService _updateService;
        private readonly VisitorSpawnService _visitorSpawnService;
        private readonly TavernUpgradePointService _tavernUpgradePointService;
        private readonly IQuantityService _visitorQuantityService;
        private readonly ILoadService _loadService;
        private readonly PauseMenuService _pauseMenuService;

        public GamePlayScene
        (
            IVolumeService volumeService,
            IAdvertisingAfterCertainPeriodService advertisingAfterCertainPeriodService,
            ISaveAfterCertainPeriodService saveAfterCertainPeriodService,
            IGameOverService gameOverService,
            IBackgroundMusicService backgroundMusicService,
            ILocalizationService localizationService,
            IFocusService focusService,
            HUD hud,
            ButtonUIFactory buttonUIFactory,
            IInputService inputService,
            IUpdateService updateService,
            VisitorSpawnService visitorSpawnService,
            TavernUpgradePointService tavernUpgradePointService,
            IQuantityService visitorQuantityService,
            PauseMenuService pauseMenuService,
            ILoadService loadService
        )
        {
            _hud = hud ? hud : throw new ArgumentNullException(nameof(hud));
            _volumeService = volumeService ?? throw new ArgumentNullException(nameof(volumeService));
            _advertisingAfterCertainPeriodService = 
                advertisingAfterCertainPeriodService ?? 
                throw new ArgumentNullException(nameof(advertisingAfterCertainPeriodService));
            _saveAfterCertainPeriodService = saveAfterCertainPeriodService ??
                                             throw new ArgumentNullException(nameof(saveAfterCertainPeriodService));
            _gameOverService = gameOverService ?? throw new ArgumentNullException(nameof(gameOverService));
            _backgroundMusicService = backgroundMusicService ??
                                      throw new ArgumentNullException(nameof(backgroundMusicService));
            _localizationService = localizationService ??
                                   throw new ArgumentNullException(nameof(localizationService));
            _focusService = focusService ?? throw new ArgumentNullException(nameof(focusService));
            _buttonUIFactory = buttonUIFactory ?? throw new ArgumentNullException(nameof(buttonUIFactory));
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
            _backgroundMusicService.Enter();
            _gameOverService.Enter();
            _saveAfterCertainPeriodService.Enter(_loadService);
            _advertisingAfterCertainPeriodService.Enter();
            _volumeService.Enter();

            _localizationService.Enter();
            _focusService.Enter();
        }

        public void Exit()
        {
            _tavernUpgradePointService.OnDisable();
            _visitorQuantityService.Exit();
            _visitorSpawnService.Exit();
            _pauseMenuService.Exit();
            _backgroundMusicService.Exit();
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