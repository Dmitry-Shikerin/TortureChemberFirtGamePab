using System;
using System.Collections.Generic;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Factories.Views.UI;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.Stores;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.Infrastructure.Services.YandexSDCServices;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.InfrastructureInterfaces.Services.SDCServices;
using Sources.Presentation.Voids;
using UnityEngine;

namespace Sources.Controllers.Scenes
{
    public class GamePlayScene : IScene
    {
        private readonly HUD _hud;
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
            ILoadService loadService)
        {
            _hud = hud ? hud : throw new ArgumentNullException(nameof(hud));
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
            _visitorQuantityService = visitorQuantityService ?? throw new ArgumentNullException(nameof(visitorQuantityService));
            _pauseMenuService = pauseMenuService ?? throw new ArgumentNullException(nameof(pauseMenuService));
            _loadService = loadService ?? throw new ArgumentNullException(nameof(loadService));
        }

        public string Name { get; } = nameof(GamePlayScene);

        public void Enter(object payload)
        {
            _loadService.Load();
            _buttonUIFactory.Create(_hud.PauseMenuWindow.SaveButton, _loadService.Save);
            _tavernUpgradePointService.OnEnable();
            _visitorQuantityService.Enter();
            _visitorSpawnService.SpawnVisitorAsync();
            _pauseMenuService.Enter();

            //SDCServices
            // _localizationService.Enter();
            // _focusService.Enter();
        }

        public void Exit()
        {
            _visitorSpawnService.Cancel();
            _tavernUpgradePointService.OnDisable();
            _visitorQuantityService.Exit();
            _visitorSpawnService.Cancel();
            _pauseMenuService.Exit();

            //SDCServices
            // _focusService.Exit();
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