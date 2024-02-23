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

        //TODO при движении с джойстика по диагонали двигается медленнее
        //TODO сделать плавный набор скорости
        public Quaternion GetDirectionRotation(Vector3 direction) =>
            Quaternion.LookRotation(direction).normalized;

        public float GetSpeedRotation() =>
            _playerMovementCharacteristic.AngularSpeed * Time.deltaTime;

        public float GetMaxSpeed(PlayerInput playerInput, float runInput)
        {
            float maxMovementValue = Mathf.Max(Mathf.Abs(playerInput.Direction.x),
                Mathf.Abs(playerInput.Direction.y));

            float speed = runInput == 0 ? maxMovementValue * 2 : maxMovementValue;

            //TODO убрать магические чиссла
            if (speed != 0)
            {
                speed += Upgradeble.CurrentLevelUpgrade.GetValue * 0.25f;
            }

            return speed;
        }

        public Vector3 GetDirection(float runInput, Vector3 cameraDirection)
        {
            float speed = runInput == 0 ? _playerMovementCharacteristic.RunSpeed : MovementSpeed;
            Vector3 direction = speed * Time.deltaTime * cameraDirection;
            direction.y -= _playerMovementCharacteristic.Gravity * Time.deltaTime;

            return direction;
        }
        
        public bool IsIdle(PlayerInput playerInput) => 
            playerInput.Direction.Approximately();
    }
}