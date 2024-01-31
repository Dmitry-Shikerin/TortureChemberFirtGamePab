using System;
using Sources.Domain.Players.Inputs;
using Sources.Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using Sources.DomainInterfaces.Upgrades;
using Sources.InfrastructureInterfaces.Services.Providers;
using UnityEngine;

namespace Sources.Infrastructure.Services.Movement
{
    public class PlayerMovementService
    {
        private IUpgradeble _upgradeble;
        private readonly IUpgradeProvider _upgradeProvider;
        private readonly PlayerMovementCharacteristic _playerMovementCharacteristic;

        public PlayerMovementService(IUpgradeProvider upgradeProvider,
            PlayerMovementCharacteristic playerMovementCharacteristic)
        {
            if (upgradeProvider == null) 
                throw new ArgumentNullException(nameof(upgradeProvider));
            //TODO костыль, нельзя делать проверку на нулл   иначе крашнется все
            // _upgradeble = upgradeProvider.Movement;
            _upgradeProvider = upgradeProvider;
            _playerMovementCharacteristic = playerMovementCharacteristic;
        }

        private IUpgradeble Upgradeble => _upgradeble ??= _upgradeProvider.Movement;

        
        private float MovementSpeed => Upgradeble.AddedAmountUpgrade;
        
        public Quaternion GetDirectionRotation(Vector3 direction) =>
            Quaternion.LookRotation(direction).normalized;

        public float GetSpeedRotation() =>
            _playerMovementCharacteristic.AngularSpeed * Time.deltaTime;

        public float GetMaxSpeed(PlayerInput playerInput, float runInput)
        {
            float maxMovementValue = Mathf.Max(Mathf.Abs(playerInput.Direction.x), 
                Mathf.Abs(playerInput.Direction.y));
            float speed = runInput == 0 ? maxMovementValue * 2 : maxMovementValue;

            return speed;
        }

        public Vector3 GetDirection(float runInput, Vector3 cameraDirection)
        {
            float speed = runInput == 0 ? _playerMovementCharacteristic.RunSpeed : MovementSpeed;
            Vector3 direction = speed * Time.deltaTime * cameraDirection;
            direction.y -= _playerMovementCharacteristic.Gravity * Time.deltaTime;

            return direction;
        }
    }
}