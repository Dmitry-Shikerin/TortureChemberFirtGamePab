using System;
using JetBrains.Annotations;
using MyProject.Sources.Domain.PlayerMovement.PlayerMovementCharacteristics;
using UnityEngine;

namespace MyProject.Sources.Domain.PlayerMovement
{
    public class PlayerMovement
    {
        private readonly PlayerMovementCharacteristic _characteristic;

        private float _movementSpeed;
        
        public PlayerMovement
        (
            PlayerMovementCharacteristic playerMovementCharacteristic
        )
        {
            _characteristic = playerMovementCharacteristic
                ? playerMovementCharacteristic
                : throw new ArgumentNullException(nameof(playerMovementCharacteristic));

            _movementSpeed = _characteristic.MovementSpeed;
        }

        public void AddMovementSpeed()
        {
            if (_movementSpeed >= 1.9f)
                throw new InvalidOperationException("Достигнут лимит улучшения скорости");
                
            _movementSpeed += 0.3f;
            Debug.Log(_movementSpeed);
        }

        public Vector3 GetDirection(float runInput, Vector3 cameraDirection)
        {
            float speed = runInput == 0 ? _characteristic.RunSpeed : _movementSpeed;
            Vector3 direction = speed * Time.deltaTime * cameraDirection;
            direction.y -= _characteristic.Gravity * Time.deltaTime;

            return direction;
        }

        public bool IsIdle(Vector2 moveInput) => 
            moveInput.x == 0.0f && moveInput.y == 0.0f;

        public Quaternion GetDirectionRotation(Vector3 direction) => 
            Quaternion.LookRotation(direction).normalized;

        public float GetSpeedRotation() => 
            _characteristic.AngularSpeed * Time.deltaTime;

        public float GetMaxSpeed(Vector2 moveInput, float runInput)
        {
            float maxMovementValue = Mathf.Max(Mathf.Abs(moveInput.x), Mathf.Abs(moveInput.y));
            float speed = runInput == 0 ? maxMovementValue * 2 : maxMovementValue;
            
            return speed;
        }
    }
}