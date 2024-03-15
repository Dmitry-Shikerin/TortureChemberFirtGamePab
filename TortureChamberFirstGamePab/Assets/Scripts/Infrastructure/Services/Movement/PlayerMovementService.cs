using System;
using Scripts.Domain.Constants;
using Scripts.Domain.Players.Inputs;
using Scripts.Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using Scripts.DomainInterfaces.Upgrades;
using Scripts.InfrastructureInterfaces.Services.Movement;
using Scripts.InfrastructureInterfaces.Services.Providers.Upgrades;
using Scripts.Utils.Extensions.MovementExtensions;
using UnityEngine;

namespace Scripts.Infrastructure.Services.Movement
{
    public class PlayerMovementService : IMovementService
    {
        private readonly PlayerMovementCharacteristic _playerMovementCharacteristic;
        private readonly IUpgradeProvider _upgradeProvider;

        private IUpgradable _upgradable;

        public PlayerMovementService(
            IUpgradeProvider upgradeProvider,
            PlayerMovementCharacteristic playerMovementCharacteristic)
        {
            _upgradeProvider = upgradeProvider ?? throw new ArgumentNullException(nameof(upgradeProvider));
            _playerMovementCharacteristic = playerMovementCharacteristic
                ? playerMovementCharacteristic
                : throw new ArgumentNullException(nameof(playerMovementCharacteristic));
        }

        private IUpgradable Upgradable => _upgradable ??= _upgradeProvider.Movement;

        private float MovementSpeed => Upgradable.CurrentAmountUpgrade;

        public Quaternion GetDirectionRotation(Vector3 direction) =>
            Quaternion.LookRotation(direction).normalized;

        public float GetSpeedRotation() =>
            _playerMovementCharacteristic.AngularSpeed * Time.deltaTime;

        public float GetMaxSpeed(PlayerInput playerInput, float currentAnimationSpeed, float runInput)
        {
            float maxMovementValue = playerInput.Direction.magnitude;

            float speed = 0;
            float delta = 6;

            if (maxMovementValue > MathfConstant.Epsilon)
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

            if (Vector3.Distance(playerInput.Direction, Vector3.zero) < MathfConstant.Epsilon)
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