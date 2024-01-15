using System;
using System.Collections.Generic;
using System.Threading;
using Sources.ControllersInterfaces;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.InfrastructureInterfaces.Factories.Services;
using Sources.Presentation.Views.Taverns.UpgradePoints;

namespace Sources.Controllers.Scenes
{
    public class GamePlayScene : IScene
    {
        private readonly IInputService _inputService;
        private readonly IUpdateService _updateService;
        private readonly VisitorSpawnService _visitorSpawnService;
        private readonly TavernUpgradePointService _tavernUpgradePointService;
        private readonly GamePlayService _gamePlayService;
        private readonly IEnumerable<PlayerUpgradeService> _playerUpgradeServices;
        
        public GamePlayScene
        (
            IInputService inputService,
            IUpdateService updateService,
            VisitorSpawnService visitorSpawnService,
            TavernUpgradePointService tavernUpgradePointService, 
            GamePlayService gamePlayService,
            IEnumerable<PlayerUpgradeService> playerUpgradeServices)
        {
            _inputService = inputService ??
                            throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _visitorSpawnService = visitorSpawnService ?? throw new ArgumentNullException(nameof(visitorSpawnService));
            _tavernUpgradePointService = tavernUpgradePointService ??
                                         throw new ArgumentNullException(nameof(tavernUpgradePointService));
            _gamePlayService = gamePlayService ?? throw new ArgumentNullException(nameof(gamePlayService));
            _playerUpgradeServices = playerUpgradeServices ?? throw new ArgumentNullException(nameof(playerUpgradeServices));
        }

        public string Name { get; } = nameof(GamePlayScene);
        
        public void Update(float deltaTime)
        {
            _inputService.Update(deltaTime);
            _updateService?.Update(deltaTime);
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

        public void Enter(object payload)
        {
            //TODO по другому не придумал
            foreach (PlayerUpgradeService playerUpgradeService in _playerUpgradeServices)
            {
                playerUpgradeService.Start();
            }
            _visitorSpawnService.SpawnVisitorAsync();
            _tavernUpgradePointService.OnEnable();
            _gamePlayService.Start();
        }

        public void Exit()
        {
            _visitorSpawnService.Cancel();
            _tavernUpgradePointService.OnDisable();
            _gamePlayService.Exit();
        }

    }
}