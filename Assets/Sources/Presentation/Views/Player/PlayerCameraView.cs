using Controllers.Player;
using PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Presentation.Views.Player
{
    public class PlayerCameraView : PresentableView<PlayerCameraPresenter>, IPlayerCameraView
    {
        private Transform _targetTransform;
        private Vector3 _targetPosition;

        public void SetTargetTransform(Transform targetTransform) =>
            _targetTransform = targetTransform;

        public void Follow() =>
            transform.position = _targetTransform.position;

        public void Rotate(float angleY) =>
            transform.rotation = Quaternion.Euler(0, angleY, 0);
    }
}