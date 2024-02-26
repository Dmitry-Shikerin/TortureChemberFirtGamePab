using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players;
using Sources.Domain.Players.PlayerMovements;
using Sources.InfrastructureInterfaces.Services.Cameras;
using Sources.InfrastructureInterfaces.Services.InputServices;
using Sources.InfrastructureInterfaces.Services.Movement;
using Sources.InfrastructureInterfaces.Services.UpdateServices.Changer;
using Sources.PresentationInterfaces.Animations;
using Sources.Utils.Extensions.MovementExtensions;
using UnityEngine;

namespace Sources.Controllers.Player
{
    public class PlayerMovementPresenter : PresenterBase
    {
        private readonly IPlayerMovementView _playerMovementView;
        private readonly IPlayerAnimation _playerAnimation;
        private readonly PlayerMovement _playerMovement;
        private readonly IInputService _inputService;
        private readonly IUpdateServiceChanger _updateService;
        private readonly ICameraDirectionService _cameraDirectionService;
        private readonly PlayerInventory _playerInventory;
        private readonly IMovementService _playerMovementService;

        public PlayerMovementPresenter
        (
            IPlayerMovementView playerMovementView,
            IPlayerAnimation playerAnimation,
            PlayerMovement playerMovement,
            IInputService inputService,
            IUpdateServiceChanger updateService,
            ICameraDirectionService cameraDirectionService,
            PlayerInventory playerInventory,
            IMovementService playerMovementService
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
            _updateService.ChangedUpdate += OnUpdate;
            _playerMovementView.SetPosition(_playerMovement.Position);
            _playerMovementView.SetAngle(_playerMovement.RotationAngle);
        }

        public override void Disable() => 
            _updateService.ChangedUpdate -= OnUpdate;

        private void OnUpdate(float deltaTime)
        {
            float runInput = 1;

            if (_playerInventory.Items.Count <= 0)
                runInput = 0;
            
            Vector3 cameraDirection = _cameraDirectionService.GetCameraDirection(
                _inputService.PlayerInput.Direction);
            
            // Vector3 direction = _playerMovementService.GetDirection(runInput, cameraDirection);

            _playerMovement.Speed = _playerMovementService.GetSpeed(
                runInput, _playerMovement.Speed, _inputService.PlayerInput);
            
            
            Vector3 direction = _playerMovementService.GetDirection(
                runInput, _playerMovement.Speed, cameraDirection);
            
            _playerMovementView.Move(direction);
            
            // float animationSpeed = _playerMovementService.GetMaxSpeed(_inputService.PlayerInput, runInput);
            
            _playerMovement.AnimationSpeed = _playerMovementService.GetMaxSpeed(
                _inputService.PlayerInput, _playerMovement.AnimationSpeed, runInput);

            
            // _playerAnimation.PlayMovementAnimation(animationSpeed);
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