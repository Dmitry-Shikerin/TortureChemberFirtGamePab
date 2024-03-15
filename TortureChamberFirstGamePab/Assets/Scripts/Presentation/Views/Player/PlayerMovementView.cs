using System;
using Scripts.Controllers.Player;
using Scripts.PresentationInterfaces.Views.Players;
using UnityEngine;

namespace Scripts.Presentation.Views.Player
{
    public class PlayerMovementView : PresentableView<PlayerMovementPresenter>, IPlayerMovementView
    {
        private CharacterController _characterController;

        public Vector3 Position => transform.position;
        public Transform Transform => transform;
        public float RotationAngle => transform.rotation.eulerAngles.y;

        private void Awake() =>
            _characterController = GetComponent<CharacterController>() ??
                                   throw new NullReferenceException(nameof(_characterController));

        public void Move(Vector3 direction) =>
            _characterController.Move(direction);

        public void Rotate(Quaternion look, float speed) =>
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, look, speed);

        public void SetPosition(Vector3 position) =>
            transform.position = position;

        public void SetAngle(float angle) =>
            transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}