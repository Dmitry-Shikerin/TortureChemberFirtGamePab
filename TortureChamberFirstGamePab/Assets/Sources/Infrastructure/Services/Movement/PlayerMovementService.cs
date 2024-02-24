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
        private IUpgradeble _upgradeble;
        private readonly IUpgradeProvider _upgradeProvider;
        private readonly PlayerMovementCharacteristic _playerMovementCharacteristic;

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

        // private float MovementSpeed => Upgradeble.AddedAmountUpgrade;
        private float MovementSpeed => Upgradeble.CurrentAmountUpgrade;

        //TODO сделать плавный набор скорости
        public Quaternion GetDirectionRotation(Vector3 direction) =>
            Quaternion.LookRotation(direction).normalized;

        public float GetSpeedRotation() =>
            _playerMovementCharacteristic.AngularSpeed * Time.deltaTime;
        
        public float GetMaxSpeed(PlayerInput playerInput, float runInput)
        {
            float maxMovementValue = playerInput.Direction.magnitude;

            float speed = runInput == 0 ? maxMovementValue * 2 : maxMovementValue;

            //TODO убрать магические чиссла
            if (speed != 0)
            {
                speed += Upgradeble.CurrentLevelUpgrade.GetValue * 0.25f;
            }

            return speed;
        }
        
        public float GetMaxSpeed(PlayerInput playerInput, float currentAnimationSpeed, float runInput)
        {
            float maxMovementValue = playerInput.Direction.magnitude;

            if (playerInput.IsSimpleAxis == false)
            {
                Vector2 normalizedMovementValue = playerInput.Direction.normalized;
                maxMovementValue = normalizedMovementValue.magnitude;
                
                Debug.Log(playerInput.IsSimpleAxis);
            }

            float speed = 0;
            
            if (maxMovementValue > 0.1f)
            {
                speed = runInput == 0 ? maxMovementValue * 2 : maxMovementValue;
                speed += Upgradeble.CurrentLevelUpgrade.GetValue;
            }
            //TODO косит в левый верхний угол
            // if (speed > 2f)
            //     speed = 2f;
            //
            // if (speed > 1 && speed < 2)
            //     speed = 1;
            
            return Mathf.MoveTowards(currentAnimationSpeed, speed, Time.deltaTime * 4);
        }

        public float GetSpeed(float runInput, float currentSpeed, PlayerInput playerInput)
        {
            float speed = runInput == 0 ? _playerMovementCharacteristic.RunSpeed : MovementSpeed;
            
            if (Vector3.Distance(playerInput.Direction, Vector3.zero) < 0.1f)
            {
                speed = 0;
            }
            
            return Mathf.MoveTowards(currentSpeed, speed, Time.deltaTime * 4);
        }

        public Vector3 GetDirection(float runInput, Vector3 cameraDirection)
        {
            float speed = runInput == 0 ? _playerMovementCharacteristic.RunSpeed : MovementSpeed;

            //TODO сделать здесь мув товардс

            Vector3 direction = speed * Time.deltaTime * cameraDirection;
            direction.y -= _playerMovementCharacteristic.Gravity * Time.deltaTime;

            return direction;
        }

        public Vector3 GetDirection(float runInput, float currentSpeed, Vector3 cameraDirection)
        {
            float speed = runInput == 0 ? _playerMovementCharacteristic.RunSpeed : MovementSpeed;

            //TODO сделать здесь мув товардс

            Vector3 direction = speed * Time.deltaTime * cameraDirection;
            direction.y -= _playerMovementCharacteristic.Gravity * Time.deltaTime;

            return direction;
        }

        public bool IsIdle(PlayerInput playerInput) =>
            playerInput.Direction.Approximately();
    }
}