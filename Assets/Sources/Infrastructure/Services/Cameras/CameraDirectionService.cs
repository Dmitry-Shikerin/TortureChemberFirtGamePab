using System;
using InfrastructureInterfaces.Services.Cameras;
using Presentation.Views.Player;
using UnityEngine;

namespace Infrastructure.Services.Cameras
{
    public class CameraDirectionService : ICameraDirectionService
    {
        private readonly PlayerCameraView _playerCameraView;

        public CameraDirectionService(PlayerCameraView playerCameraView)
        {
            _playerCameraView = playerCameraView
                ? playerCameraView
                : throw new ArgumentNullException(nameof(playerCameraView));
        }

        public Vector3 GetCameraDirection(Vector2 moveInput)
        {
            Vector3 direction = _playerCameraView.transform.TransformDirection(
                moveInput.x, 0, moveInput.y).normalized;

            return direction;
        }
    }
}