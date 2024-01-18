using System;
using MyProject.Sources.PresentationInterfaces.Views;
using Sources.Controllers.Player;
using UnityEngine;

namespace Sources.Presentation.Views.Player
{
    public class PlayerMovementView : PresentableView<PlayerMovementPresenter>, IPlayerMovementView
    {
        private CharacterController _characterController;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>() ??
                                   throw new NullReferenceException(nameof(_characterController));
        }

        public Vector3 Position => transform.position;
        public Transform Transform => transform;

        public void Move(Vector3 direction) => 
            _characterController.Move(direction);

        public void Rotate(Quaternion look, float speed) =>
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, look, speed);
    }
}