using System;
using JetBrains.Annotations;
using MyProject.Sources.Controllers.Common;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Domain.Players.PlayerCameras;
using Sources.Infrastructure.Services;
using Sources.InfrastructureInterfaces.Factories.Services;

namespace MyProject.Sources.Controllers
{
    public class PlayerCameraPresenter : PresenterBase
    {
        private readonly PlayerCamera _playerCamera;
        private readonly IPlayerCameraView _playerCameraView;
        private readonly IInputService _inputService;
        private readonly IUpdateService _updateService;

        public PlayerCameraPresenter
        (
            PlayerCamera playerCamera,
            IPlayerCameraView playerCameraView,
            IInputService inputService,
            IUpdateService updateService
        )
        {
            _playerCamera = playerCamera ?? 
                            throw new ArgumentNullException(nameof(playerCamera));
            _playerCameraView = playerCameraView ?? 
                                throw new ArgumentNullException(nameof(playerCameraView));
            _inputService = inputService ??
                throw new ArgumentNullException(nameof(inputService));
            _updateService = updateService ?? throw new ArgumentNullException(nameof(updateService));
        }

        public override void Enable()
        {
            _inputService.RotationChanged += OnRotationChanged;
            _updateService.ChangedLateUpdate += OnLateUpdate;
        }
        
        public override void Disable()
        {
            _inputService.RotationChanged -= OnRotationChanged;
            _updateService.ChangedLateUpdate -= OnLateUpdate;
        }

        private void OnLateUpdate(float deltaTime)
        {
            _playerCameraView.Follow();
            _playerCameraView.Rotate(_playerCamera.AngleY);
        }
        
        private void OnRotationChanged(bool isLeftRotation, bool isRightRotation)
        {
            if(isLeftRotation)
                _playerCamera.SetLeftRotation();
            
            if(isRightRotation)
                _playerCamera.SetRightRotation();
        }
    }
}