using System;
using Scripts.Domain.Players.PlayerCameras;
using Scripts.InfrastructureInterfaces.Services.InputServices;
using Scripts.InfrastructureInterfaces.Services.UpdateServices.Changer;
using Scripts.PresentationInterfaces.Views.Players;

namespace Scripts.Controllers.Player
{
    public class PlayerCameraPresenter : PresenterBase
    {
        private readonly IInputService _inputService;
        private readonly PlayerCamera _playerCamera;
        private readonly IPlayerCameraView _playerCameraView;
        private readonly IUpdateServiceChanger _updateService;

        public PlayerCameraPresenter(
            PlayerCamera playerCamera,
            IPlayerCameraView playerCameraView,
            IInputService inputService,
            IUpdateServiceChanger updateService)
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
            if (isLeftRotation)
                _playerCamera.SetLeftRotation();
            else if (isRightRotation)
                _playerCamera.SetRightRotation();
        }
    }
}