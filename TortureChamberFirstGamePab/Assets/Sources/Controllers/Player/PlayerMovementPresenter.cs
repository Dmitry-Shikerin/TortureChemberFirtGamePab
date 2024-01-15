using System;
using MyProject.Sources.Controllers.Common;
using MyProject.Sources.Domain.PlayerMovement;
using MyProject.Sources.PresentationInterfaces.Animations;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Cameras;
using Sources.InfrastructureInterfaces.Factories.Services;
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
            PlayerInventory playerInventory
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
        }

        public override void Enable()
        {
            _inputService.MovementAxisChanged += OnMovementAxis;
            _inputService.RunAxisChanged += OnRunAxis;
            _updateService.ChangedUpdate += OnUpdate;
            _playerMovement.PositionChanged += OnPositionChanged;
        }

        public override void Disable()
        {
            _inputService.MovementAxisChanged -= OnMovementAxis;
            _inputService.RunAxisChanged -= OnRunAxis;
            _updateService.ChangedUpdate -= OnUpdate;
            _playerMovement.PositionChanged += OnPositionChanged;
        }

        private void OnPositionChanged()
        {
            float runInput = 1;

            if (_playerInventory.Items.Count <= 0)
                runInput = 0;
            
            Vector3 cameraDirection = _cameraDirectionService.GetCameraDirection(_movementInput);
            Vector3 direction = _playerMovement.GetDirection(runInput, cameraDirection);

            float animationSpeed = _playerMovement.GetMaxSpeed(_movementInput, runInput);

            _playerAnimation.PlayMovementAnimation(animationSpeed);
            _playerMovementView.Move(_playerMovement.Position);
        }

        private void OnUpdate(float deltaTime)
        {
            // float runInput = 1;
            //
            // if (_playerInventory.Items.Count <= 0)
            //     runInput = 0;
            //
            Vector3 cameraDirection = _cameraDirectionService.GetCameraDirection(_movementInput);
            // Vector3 direction = _playerMovement.GetDirection(runInput, cameraDirection);
            //
            // float animationSpeed = _playerMovement.GetMaxSpeed(_movementInput, runInput);
            //
            // _playerAnimation.PlayMovementAnimation(animationSpeed);
            // _playerMovementView.Move(direction);

            if (_playerMovement.IsIdle(_movementInput))
                return;

            Quaternion look = _playerMovement.GetDirectionRotation(cameraDirection);
            float speedRotation = _playerMovement.GetSpeedRotation();

            _playerMovementView.Rotate(look, speedRotation);
        }

        private void OnRunAxis(float runInput) =>
            _runInput = runInput;

        private void OnMovementAxis(Vector2 movementInput) =>
            _movementInput = movementInput;
    }
}