using System;
using MyProject.Sources.Controllers;
using MyProject.Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace MyProject.Sources.Presentation.Views
{
    public class PlayerCameraView : PresentableView<PlayerCameraPresenter>, IPlayerCameraView
    {
        private Transform _targetTransform;

        public void SetTargetTransform(PlayerMovementView playerMovementView)
        {
            _targetTransform = playerMovementView.transform
                ? playerMovementView.transform
                : throw new ArgumentNullException(nameof(playerMovementView));
        }

        public void Follow()
        {
            transform.position = _targetTransform.position;
        }

        public void Rotate(float angleY)
        {
            transform.rotation = Quaternion.Euler(0, angleY, 0);
        }
    }
}