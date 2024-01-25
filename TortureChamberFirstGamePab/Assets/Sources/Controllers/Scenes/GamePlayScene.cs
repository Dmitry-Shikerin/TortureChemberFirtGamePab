using System;
using System.Collections.Generic;
using Sources.ControllersInterfaces.Scenes;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.LoadServices;
using Sources.Infrastructure.Services.LoadServices.Components;
using Sources.Infrastructure.Services.Stores;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.InputServices;
using UnityEngine;

namespace Sources.Controllers.Scenes
{
    public class GamePlayScene : IScene
    {
        private readonly IInputService _inputService;
        private readonly IUpdateService _updateService;
        private readonly VisitorSpawnService _visitorSpawnService;
        private readonly TavernUpgradePointService _tavernUpgradePointService;
        private readonly GamePlayService _gamePlayService;
        private readonly ILoadService _loadService;
        private readonly PauseMenuService _pauseMenuService;

        public GamePlayScene
        (
            IInputService inputService,
            IUpdateService updateService,
            VisitorSpawnService visitorSpawnService,
            TavernUpgradePointService tavernUpgradePointService,
            GamePlayService gamePlayService,
            PauseMenuService pauseMenuService,
            ILoadService loadService
        )
        {
            _inputService = inputService ??
                            throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _visitorSpawnService = visitorSpawnService ?? throw new ArgumentNullException(nameof(visitorSpawnService));
            _tavernUpgradePointService = tavernUpgradePointService ??
                                         throw new ArgumentNullException(nameof(tavernUpgradePointService));
            _gamePlayService = gamePlayService ?? throw new ArgumentNullException(nameof(gamePlayService));
            _pauseMenuService = pauseMenuService ?? throw new ArgumentNullException(nameof(pauseMenuService));
            _loadService = loadService ?? throw new ArgumentNullException(nameof(loadService));
        }

        public string Name { get; } = nameof(GamePlayScene);

        public void Enter(object payload)
        {
            _loadService.Load();
            _tavernUpgradePointService.OnEnable();
            _gamePlayService.Start();
            _visitorSpawnService.SpawnVisitorAsync();
            _pauseMenuService.Enter();
        }

        public void Exit()
        {
            _visitorSpawnService.Cancel();
            _tavernUpgradePointService.OnDisable();
            _gamePlayService.Exit();
            _visitorSpawnService.Cancel();
            _pauseMenuService.Exit();
        }

        public void Update(float deltaTime)
        {
            _inputService.Update(deltaTime);
            _updateService.Update(deltaTime);

            if (Input.GetKeyDown(KeyCode.Z))
            {
                _loadService.Save();
            }
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