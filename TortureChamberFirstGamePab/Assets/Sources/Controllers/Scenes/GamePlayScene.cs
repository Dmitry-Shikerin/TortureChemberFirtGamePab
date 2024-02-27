using System;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.InfrastructureInterfaces.Services.LoadServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.InfrastructureInterfaces.Services.VolumeServices;
using Sources.Presentation.Voids;
using UnityEngine;

namespace Sources.Controllers.Scenes
{
    public class GamePlayScene : IScene
    {
        private readonly HUD _hud;
        private readonly IMobilePlatformService _mobilePlatformService;
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
            IMobilePlatformService mobilePlatformService,
            IVolumeService volumeService,
            IAdvertisingAfterCertainPeriodService advertisingAfterCertainPeriodService,
            ISaveAfterCertainPeriodService saveAfterCertainPeriodService,
            IGameOverService gameOverService,
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

        //TODO сделать проверку на наличие сохраненией в сеттинге если их нет сделать громкость 0.2
        //TODO музыка не меняет громкость 
        //TODO луп звуки не ставятся на паузу
        public void Enter(object payload)
        {
            Debug.Log("Start Enter GamePlay");
            _loadService.Load();
            Debug.Log("Enter LoadService");
            _tavernUpgradePointService.OnEnable();
            Debug.Log("Enter TavernUpgradePointService");
            _visitorQuantityService.Enter();
            Debug.Log("Enter VisitorQuantityService");
            _visitorSpawnService.Enter();
            Debug.Log("Enter VisitorSpawnService");
            _pauseMenuService.Enter();
            Debug.Log("Enter PauseMenuService");
            _gameOverService.Enter();
            Debug.Log("Enter GameOverService");
            _saveAfterCertainPeriodService.Enter(_loadService);
            Debug.Log("Enter SaveAfterCertainPeriodService");
            _advertisingAfterCertainPeriodService.Enter();
            Debug.Log("Enter _advertisingAfterCertainPeriodService");
            _volumeService.Enter();
            Debug.Log("Enter _volumeService");
            _mobilePlatformService.Enter();
            Debug.Log("Enter _mobilePlatformService");

            _localizationService.Enter();
            Debug.Log("Enter _localizationService");
            _focusService.Enter();
            Debug.Log("End Enter GamePlay");
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