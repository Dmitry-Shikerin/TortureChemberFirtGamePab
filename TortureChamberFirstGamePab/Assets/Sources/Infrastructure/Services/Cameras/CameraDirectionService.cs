using System;
using JetBrains.Annotations;
using MyProject.Sources.Presentation.Views;
using UnityEngine;

namespace Sources.Infrastructure.Services.Cameras
{
    public class CameraDirectionService
    {
        private readonly PlayerCameraView _playerCameraView;

        public CameraDirectionService(PlayerCameraView playerCameraView)
        {
            _playerCameraView = playerCameraView ? 
                playerCameraView : 
                throw new ArgumentNullException(nameof(playerCameraView));
        }
        
        public Vector3 GetCameraDirection(Vector2 moveInput)
        {
            Vector3 direction = _playerCameraView.transform.TransformDirection(
                moveInput.x, 0, moveInput.y).normalized;

            // direction.y -= _characteristic.Gravity * Time.deltaTime;

            return direction;
        }

    }
}