using System;
using Sources.Domain.Players.Inputs;
using Sources.Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using Sources.DomainInterfaces.Upgrades;
using Sources.InfrastructureInterfaces.Services.Movement;
using Sources.InfrastructureInterfaces.Services.Providers;
using Sources.Utils.Extensions.MovementExtensions;
using UnityEngine;

namespace Sources.Infrastructure.Services.Movement
{
    public class PlayerMovementService : IMovementService
    {
        private readonly IUpgradeProvider _upgradeProvider;
        private readonly PlayerMovementCharacteristic _playerMovementCharacteristic;

        private IUpgradeble _upgradeble;
        
        public PlayerMovementService
        (
            IUpgradeProvider upgradeProvider,
            PlayerMovementCharacteristic playerMovementCharacteristic
        )
        {
            _upgradeProvider = upgradeProvider ?? throw new ArgumentNullException(nameof(upgradeProvider));
            _playerMovementCharacteristic = playerMovementCharacteristic
                ? playerMovementCharacteristic
                : throw new ArgumentNullException(nameof(playerMovementCharacteristic));
        }

        private IUpgradeble Upgradeble => _upgradeble ??= _upgradeProvider.Movement;

        private float MovementSpeed => Upgradeble.CurrentAmountUpgrade;

        public Quaternion GetDirectionRotation(Vector3 direction) =>
            Quaternion.LookRotation(direction).normalized;

        public float GetSpeedRotation() =>
            _playerMovementCharacteristic.AngularSpeed * Time.deltaTime;
        
        public float GetMaxSpeed(PlayerInput playerInput, float currentAnimationSpeed, float runInput)
        {
            float maxMovementValue = playerInput.Direction.magnitude;

            float speed = 0;
            float delta = 6;
            
            if (maxMovementValue > 0.1f)
            {
                speed = runInput == 0 ? maxMovementValue * 2 : maxMovementValue;
                delta = 4;
            }
            
            return Mathf.MoveTowards(currentAnimationSpeed, speed, Time.deltaTime * delta);
        }

        public float GetSpeed(float runInput, float currentSpeed, PlayerInput playerInput)
        {
            float delta = 4;
            
            float speed = runInput == 0 ? _playerMovementCharacteristic.RunSpeed : MovementSpeed;
            
            if (Vector3.Distance(playerInput.Direction, Vector3.zero) < 0.1f)
            {
                speed = 0;
                delta = 6;
            }
            
            return Mathf.MoveTowards(currentSpeed, speed, Time.deltaTime * delta);
        }

        public Vector3 GetDirection(float runInput, float currentSpeed, Vector3 cameraDirection)
        {
            Vector3 direction = currentSpeed * Time.deltaTime * cameraDirection;
            
            direction.y -= _playerMovementCharacteristic.Gravity * Time.deltaTime;

            return direction;
        }

        public bool IsIdle(PlayerInput playerInput) =>
            playerInput.Direction.Approximately();
    }
}