using System;
using UnityEngine;

namespace Sources.Infrastructure.Services
{
    public class InputService : IInputService
    {
        public event Action<Vector2> MovementAxisChanged;
        public event Action<float> RunAxisChanged;
        public event Action<bool, bool> RotationChanged;

        //TODO Может быть сделать для инпута вьюшку и подписаться на updateService?
        public void Update(float deltaTime)
        {
            UpdateMovementAxis();
            UpdateRunAxis();
        }

        public void UpdateLate(float deltaTime)
        {
            UpdateRotation();
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }

        private void UpdateMovementAxis()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 movementInput = new Vector2(horizontalInput, verticalInput);
        
            MovementAxisChanged?.Invoke(movementInput);
        }

        private void UpdateRunAxis()
        {
            float runInput = Input.GetAxis("Run");
        
            RunAxisChanged?.Invoke(runInput);
        }

        private void UpdateRotation()
        {
            bool isLeftRotation = Input.GetKey(KeyCode.Q);
            bool isRightRotation = Input.GetKey(KeyCode.E);
        
            RotationChanged?.Invoke(isLeftRotation, isRightRotation);
        }
    }
}