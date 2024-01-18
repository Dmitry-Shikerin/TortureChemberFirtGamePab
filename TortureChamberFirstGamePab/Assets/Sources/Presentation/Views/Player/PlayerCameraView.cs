using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Player;
using UnityEngine;

namespace Sources.Presentation.Views.Player
{
    public class PlayerCameraView : PresentableView<PlayerCameraPresenter>, IPlayerCameraView
    {
        private Vector3 _tragetPosition;
        private Transform _targetTransform;

        public void SetTargetTransform(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            Debug.Log(targetTransform);
        }

        public void Follow()
        {
            transform.position = _targetTransform.position;
        }

        public void Rotate(float angleY)
        {
            transform.rotation = Quaternion.Euler(0, angleY, 0);
        }

        public void SetTargetTransform(IPlayerMovementView playerMovementView)
        {
            throw new NotImplementedException();
        }
    }
}