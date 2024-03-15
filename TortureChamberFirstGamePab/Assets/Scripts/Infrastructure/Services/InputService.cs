using System;
using Scripts.Domain.Constants;
using Scripts.Domain.Players.Inputs;
using Scripts.InfrastructureInterfaces.Services.InputServices;
using UnityEngine;

namespace Scripts.Infrastructure.Services
{
    public class InputService : IInputService
    {
        public event Action<bool, bool> RotationChanged;
        public event Action PauseButtonChanged;

        public PlayerInput PlayerInput { get; private set; }

        public void Update(float deltaTime)
        {
            UpdateMovementAxis();
            UpdatePauseInput();
        }

        public void UpdateLate(float deltaTime) =>
            UpdateRotation();

        public void UpdateFixed(float fixedDeltaTime)
        {
        }

        private void UpdateMovementAxis()
        {
            Vector2 direction = new Vector2(
                Input.GetAxis(InputConstant.Horizontal),
                Input.GetAxis(InputConstant.Vertical));

            if (direction.sqrMagnitude < 0.1f)
                UpdateFromSimpleInput();
            else
                UpdateFromKeyBoard();
        }

        private void UpdateFromKeyBoard()
        {
            Vector2 direction = new Vector2(
                Input.GetAxis(InputConstant.Horizontal),
                Input.GetAxis(InputConstant.Vertical));

            direction = direction.magnitude < 0.1f ? Vector2.zero : direction.normalized;

            PlayerInput = new PlayerInput(direction);
        }

        private void UpdateFromSimpleInput()
        {
            Vector2 direction = new Vector2(
                SimpleInput.GetAxis(InputConstant.Horizontal),
                SimpleInput.GetAxis(InputConstant.Vertical));

            PlayerInput = new PlayerInput(direction);
        }

        private void UpdateRotation()
        {
            bool isLeftRotation = Input.GetKey(KeyCode.Q);
            bool isRightRotation = Input.GetKey(KeyCode.E);

            if (isLeftRotation == false && isRightRotation == false)
                UpdateSimpleInputRotation();
            else
                UpdateStandaloneRotation();
        }

        private void UpdateStandaloneRotation()
        {
            bool isLeftRotation = Input.GetKey(KeyCode.Q);
            bool isRightRotation = Input.GetKey(KeyCode.E);

            RotationChanged?.Invoke(isLeftRotation, isRightRotation);
        }

        private void UpdateSimpleInputRotation()
        {
            float rotation = SimpleInput.GetAxis(InputConstant.Rotation);

            bool isLeftRotation = rotation > MathfConstant.Epsilon;
            bool isRightRotation = rotation < -MathfConstant.Epsilon;

            RotationChanged?.Invoke(isLeftRotation, isRightRotation);
        }

        private void UpdatePauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseButtonChanged?.Invoke();
        }
    }
}