using System;
using Domain.Players;
using Domain.Players.PlayerMovements;
using InfrastructureInterfaces.Services.Cameras;
using InfrastructureInterfaces.Services.InputServices;
using InfrastructureInterfaces.Services.Movement;
using InfrastructureInterfaces.Services.UpdateServices.Changer;
using PresentationInterfaces.Animations;
using PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovementPresenter : PresenterBase
    {
        private readonly ICameraDirectionService _cameraDirectionService;
        private readonly IInputService _inputService;
        private readonly IPlayerAnimation _playerAnimation;
        private readonly PlayerInventory _playerInventory;
        private readonly PlayerMovement _playerMovement;
        private readonly IMovementService _playerMovementService;
        private readonly IPlayerMovementView _playerMovementView;
        private readonly IUpdateServiceChanger _updateService;

        public PlayerMovementPresenter(
            IPlayerMovementView playerMovementView,
            IPlayerAnimation playerAnimation,
            PlayerMovement playerMovement,
            IInputService inputService,
            IUpdateServiceChanger updateService,
            ICameraDirectionService cameraDirectionService,
            PlayerInventory playerInventory,
            IMovementService playerMovementService)
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
            _playerMovementService =
                playerMovementService ?? throw new ArgumentNullException(nameof(playerMovementService));
        }

        public override void Enable()
        {
            _updateService.ChangedUpdate += OnUpdate;
            _playerMovement.Position = _playerMovementView.Position;
            _playerMovementView.SetAngle(_playerMovement.RotationAngle);
        }

        public override void Disable()
        {
            _updateService.ChangedUpdate -= OnUpdate;
        }

        private void OnUpdate(float deltaTime)
        {
            float runInput = 1;

            if (_playerInventory.Items.Count <= 0)
                runInput = 0;

            Vector3 cameraDirection = _cameraDirectionService.GetCameraDirection(
                _inputService.PlayerInput.Direction);

            _playerMovement.Speed = _playerMovementService.GetSpeed(
                runInput, _playerMovement.Speed, _inputService.PlayerInput);

            Vector3 direction = _playerMovementService.GetDirection(
                runInput, _playerMovement.Speed, cameraDirection);

            _playerMovementView.Move(direction);

            _playerMovement.AnimationSpeed = _playerMovementService.GetMaxSpeed(
                _inputService.PlayerInput, _playerMovement.AnimationSpeed, runInput);

            _playerAnimation.PlayMovementAnimation(_playerMovement.AnimationSpeed);

            if (_playerMovementService.IsIdle(_inputService.PlayerInput))
                return;

            Quaternion look = _playerMovementService.GetDirectionRotation(cameraDirection);
            float speedRotation = _playerMovementService.GetSpeedRotation();

            _playerMovementView.Rotate(look, speedRotation);

            _playerMovement.Position = _playerMovementView.Position;
            _playerMovement.RotationAngle = _playerMovementView.RotationAngle;
        }
    }
}