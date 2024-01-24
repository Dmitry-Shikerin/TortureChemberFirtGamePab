using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.Infrastructure.Services.Cameras;
using Sources.Infrastructure.Services.Movement;
using Sources.InfrastructureInterfaces.Services;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.PresentationInterfaces.Animations;
using UnityEngine;

namespace Sources.Controllers.Player
{
    public class PlayerMovementPresenter : PresenterBase
    {
        private readonly IPlayerMovementView _playerMovementView;
        private readonly IPlayerAnimation _playerAnimation;
        private readonly PlayerMovement _playerMovement;
        private readonly IInputService _inputService;
        private readonly IUpdateService _updateService;
        private readonly CameraDirectionService _cameraDirectionService;
        private readonly PlayerInventory _playerInventory;
        private readonly PlayerMovementService _playerMovementService;

        private float _runInput;
        private Vector2 _movementInput;

        public PlayerMovementPresenter
        (
            IPlayerMovementView playerMovementView,
            IPlayerAnimation playerAnimation,
            PlayerMovement playerMovement,
            IInputService inputService,
            IUpdateService updateService,
            CameraDirectionService cameraDirectionService,
            PlayerInventory playerInventory,
            PlayerMovementService playerMovementService
        )
        {
            _playerMovementView = playerMovementView ??
                                  throw new ArgumentNullException(nameof(playerMovementView));
            _playerAnimation = playerAnimation ??
                               throw new ArgumentNullException(nameof(playerAnimation));
            _playerMovement = playerMovement ??
                              throw new ArgumentNullException(nameof(playerMovement));
            _inputService = inputService ??
                            throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
            _cameraDirectionService = cameraDirectionService ??
                                      throw new ArgumentNullException(nameof(cameraDirectionService));
            _playerInventory = playerInventory ?? throw new ArgumentNullException(nameof(playerInventory));
            _playerMovementService = playerMovementService ?? throw new ArgumentNullException(nameof(playerMovementService));
        }

        public override void Enable()
        {
            _inputService.MovementAxisChanged += OnMovementAxis;
            _inputService.RunAxisChanged += OnRunAxis;
            _updateService.ChangedUpdate += OnUpdate;
            _playerMovementView.SetPosition(_playerMovement.Position);
            _playerMovementView.SetAngle(_playerMovement.RotationAngle);
        }

        public override void Disable()
        {
            _inputService.MovementAxisChanged -= OnMovementAxis;
            _inputService.RunAxisChanged -= OnRunAxis;
            _updateService.ChangedUpdate -= OnUpdate;
        }


        private void OnUpdate(float deltaTime)
        {
            float runInput = 1;

            if (_playerInventory.Items.Count <= 0)
                runInput = 0;
            
            Vector3 cameraDirection = _cameraDirectionService.GetCameraDirection(
                _inputService.PlayerInput.Direction);
            Vector3 direction = _playerMovementService.GetDirection(runInput, cameraDirection);
            
            _playerMovementView.Move(direction);
            
            float animationSpeed = _playerMovementService.GetMaxSpeed(_inputService.PlayerInput, runInput);

            _playerAnimation.PlayMovementAnimation(animationSpeed);
            
            if (_playerMovement.IsIdle(_inputService.PlayerInput.Direction))
                return;

            Quaternion look = _playerMovementService.GetDirectionRotation(cameraDirection);
            float speedRotation = _playerMovementService.GetSpeedRotation();

            _playerMovementView.Rotate(look, speedRotation);

            _playerMovement.Position = _playerMovementView.Position;
            _playerMovement.RotationAngle = _playerMovementView.RotationAngle;
        }

        private void OnRunAxis(float runInput) =>
            _runInput = runInput;

        private void OnMovementAxis(Vector2 movementInput) =>
            _movementInput = movementInput;
    }
}