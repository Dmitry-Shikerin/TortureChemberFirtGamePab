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

        public void UpdateLate(float deltaTime) => 
            UpdateRotation();

        public void UpdateFixed(float fixedDeltaTime)
        {
        }

        private void UpdateMovementAxis()
        {
            float horizontalInput = Input.GetAxis(Constant.Input.Horizontal);
            float verticalInput = Input.GetAxis(Constant.Input.Vertical);

            if (SimpleInput.GetAxisSnapValue)
            {
                horizontalInput = SimpleInput.GetAxis(Constant.Input.Horizontal);
                verticalInput = SimpleInput.GetAxis(Constant.Input.Vertical);
            }

            PlayerInput = new PlayerInput(new Vector2(horizontalInput, verticalInput));
        }

        private void UpdateRotation()
        {
            bool isLeftRotation = Input.GetKey(KeyCode.Q);
            bool isRightRotation = Input.GetKey(KeyCode.E);

            if (SimpleInput.GetAxisSnapValue)
            {
                float rotation = SimpleInput.GetAxis(Constant.Input.Rotation);
                
                if (rotation > Constant.Epsilon)
                    isLeftRotation = true;
                
                if (rotation < -Constant.Epsilon)
                    isRightRotation = true;
            }

            RotationChanged?.Invoke(isLeftRotation, isRightRotation);
        }

        private void UpdatePauseInput()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                PauseButtonChanged?.Invoke();
        }
    }
}