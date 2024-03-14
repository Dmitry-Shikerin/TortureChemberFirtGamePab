using System;
using Sources.Domain.Constants;
using Sources.Domain.Players.Inputs;
using Sources.InfrastructureInterfaces.Services.InputServices;
using UnityEngine;

namespace Sources.Infrastructure.Services
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

        public void UpdateLate(float deltaTime)
        {
            UpdateRotation();
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }

        private void UpdateMovementAxis()
        {
            var direction = new Vector2(Input.GetAxis(Constant.Input.Horizontal),
                Input.GetAxis(Constant.Input.Vertical));

            if (direction.sqrMagnitude < 0.1f)
                UpdateFromSimpleInput();
            else
                UpdateFromKeyBoard();
        }

        private void UpdateFromKeyBoard()
        {
            var direction = new Vector2(Input.GetAxis(Constant.Input.Horizontal),
                Input.GetAxis(Constant.Input.Vertical));

            direction = direction.magnitude < 0.1f ? Vector2.zero : direction.normalized;

            PlayerInput = new PlayerInput(direction, false);
        }

        private void UpdateFromSimpleInput()
        {
            var direction = new Vector2(SimpleInput.GetAxis(Constant.Input.Horizontal),
                SimpleInput.GetAxis(Constant.Input.Vertical));

            PlayerInput = new PlayerInput(direction, true);
        }

        private void UpdateRotation()
        {
            var isLeftRotation = Input.GetKey(KeyCode.Q);
            var isRightRotation = Input.GetKey(KeyCode.E);

            if (isLeftRotation == false && isRightRotation == false)
                UpdateSimpleInputRotation();
            else
                UpdateStandaloneRotation();
        }

        private void UpdateStandaloneRotation()
        {
            var isLeftRotation = Input.GetKey(KeyCode.Q);
            var isRightRotation = Input.GetKey(KeyCode.E);

            RotationChanged?.Invoke(isLeftRotation, isRightRotation);
        }

        private void UpdateSimpleInputRotation()
        {
            var rotation = SimpleInput.GetAxis(Constant.Input.Rotation);

            var isLeftRotation = rotation > Constant.Epsilon;
            var isRightRotation = rotation < -Constant.Epsilon;

            RotationChanged?.Invoke(isLeftRotation, isRightRotation);
        }

        private void UpdatePauseInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseButtonChanged?.Invoke();
        }
    }
}