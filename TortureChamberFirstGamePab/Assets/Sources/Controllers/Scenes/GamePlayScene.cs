using System;
using System.Collections.Generic;
using MyProject.Sources.Presentation.Views;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.ControllersInterfaces;
using Sources.ControllersInterfaces.Scenes;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerCameras;
using Sources.Domain.Players.PlayerMovements;
using Sources.Domain.Starables;
using Sources.Infrastructure.Factories.Views.Players;
using Sources.Infrastructure.Repositories;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Stores;
using Sources.Infrastructure.Services.UpgradeServices;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.Presentation.Views.Player;
using Sources.Presentation.Views.Taverns.UpgradePoints;
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
        private readonly IEnumerable<PlayerUpgradeService> _playerUpgradeServices;
        private readonly StoreService _storeService;
        private readonly StorableRepository _storableRepository;
        private readonly PlayerMovementViewFactory _playerMovementViewFactory;
        private readonly PlayerCameraViewFactory _playerCameraViewFactory;
        private readonly PauseMenuService _pauseMenuService;

        public GamePlayScene
        (IInputService inputService,
            IUpdateService updateService,
            VisitorSpawnService visitorSpawnService,
            TavernUpgradePointService tavernUpgradePointService,
            GamePlayService gamePlayService,
            IEnumerable<PlayerUpgradeService> playerUpgradeServices,
            StoreService storeService,
            StorableRepository storableRepository,
            PlayerMovementViewFactory playerMovementViewFactory,
            //TODO может сюда прокинуть вью репозитори?
            PlayerCameraViewFactory playerCameraViewFactory,
            PauseMenuService pauseMenuService
        )
        {
            _inputService = inputService ??
                            throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _visitorSpawnService = visitorSpawnService ?? throw new ArgumentNullException(nameof(visitorSpawnService));
            _tavernUpgradePointService = tavernUpgradePointService ??
                                         throw new ArgumentNullException(nameof(tavernUpgradePointService));
            _gamePlayService = gamePlayService ?? throw new ArgumentNullException(nameof(gamePlayService));
            _playerUpgradeServices = playerUpgradeServices ?? throw new ArgumentNullException(nameof(playerUpgradeServices));
            _storeService = storeService ?? throw new ArgumentNullException(nameof(storeService));
            _storableRepository = storableRepository ?? throw new ArgumentNullException(nameof(storableRepository));
            _playerMovementViewFactory = playerMovementViewFactory ?? throw new ArgumentNullException(nameof(playerMovementViewFactory));
            _playerCameraViewFactory = playerCameraViewFactory ?? throw new ArgumentNullException(nameof(playerCameraViewFactory));
            _pauseMenuService = pauseMenuService ?? throw new ArgumentNullException(nameof(pauseMenuService));
        }

        public string Name { get; } = nameof(GamePlayScene);
        
        public void Update(float deltaTime)
        {
            _inputService.Update(deltaTime);
            _updateService.Update(deltaTime);

            if (Input.GetKey(KeyCode.Z))
            {
                _storeService.Save();
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
            _pauseMenuService.Enter();

            //TODO в фабрике мы создаем фабрики а здесь создаем обьекты?
            PlayerMovement playerMovement = new PlayerMovement();
            PlayerInventory playerInventory = new PlayerInventory();
            
            //TODO потом раскоментировать
            // IPlayerMovementView playerMovementView = _playerMovementViewFactory.Create(playerMovement);
            
            _storeService.Load();
            
            PlayerInventoryStorable playerInventoryStorable = new PlayerInventoryStorable(playerInventory);
            _storableRepository.Add(playerInventoryStorable);
            
            //TODO плохая идея брать так зависимость
            PlayerMovementStorable playerMovementStorable = 
                new PlayerMovementStorable(playerMovement, playerInventoryStorable.PlayerInventory);
            _storableRepository.Add(playerMovementStorable);
            
            PlayerMovementView playerMovementView = playerMovementStorable.PlayerMovementView
                ? playerMovementStorable.PlayerMovementView :
                _playerMovementViewFactory.Create(playerMovement, playerInventory);
            
            PlayerCamera playerCamera = new PlayerCamera();
            IPlayerCameraView playerCameraView = _playerCameraViewFactory.Create(playerCamera);
            playerCameraView.SetTargetTransform(playerMovementView.Transform);

            //TODO загрузка стор сервиса
            // _storeService.Load();
        }

        public void Exit()
        {
            _visitorSpawnService.Cancel();
            _tavernUpgradePointService.OnDisable();
            _gamePlayService.Exit();
            _pauseMenuService.Exit();
        }
    }
}