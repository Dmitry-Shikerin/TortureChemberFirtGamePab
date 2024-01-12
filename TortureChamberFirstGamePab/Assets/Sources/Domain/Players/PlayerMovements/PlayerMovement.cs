using System;
using MyProject.Sources.Domain.PlayerMovement.PlayerMovementCharacteristics;
using Sources.DomainInterfaces.Upgrades;
using UnityEngine;

namespace MyProject.Sources.Domain.PlayerMovement
{
    public class PlayerMovement
    {
        private readonly IUpgradeble _upgradeble;
        private readonly PlayerMovementCharacteristic _characteristic;
        
        public PlayerMovement
        (
            PlayerMovementCharacteristic playerMovementCharacteristic,
            IUpgradeble upgradeble
        )
        {
            _upgradeble = upgradeble ?? throw new ArgumentNullException(nameof(upgradeble));
            _characteristic = playerMovementCharacteristic
                ? playerMovementCharacteristic
                : throw new ArgumentNullException(nameof(playerMovementCharacteristic));

            MovementSpeed = _upgradeble.AddedAmountUpgrade;
        }
        public float MovementSpeed { get; private set; }
        
        public Vector3 GetDirection(float runInput, Vector3 cameraDirection)
        {
            float speed = runInput == 0 ? _characteristic.RunSpeed : MovementSpeed;
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