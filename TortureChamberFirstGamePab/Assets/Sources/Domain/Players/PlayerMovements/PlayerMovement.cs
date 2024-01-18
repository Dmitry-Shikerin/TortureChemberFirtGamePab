using System;
using Sources.Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using Sources.DomainInterfaces.Upgrades;
using UnityEngine;

namespace Sources.Domain.Players.PlayerMovements
{
    public class PlayerMovement
    {
        private readonly IUpgradeble _upgradeble;
        private readonly PlayerMovementCharacteristic _characteristic;

        private Vector3 _position;

        public event Action PositionChanged;

        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = new Vector3(value.x, value.y, value.z);
                PositionChanged?.Invoke();
            }
        }

        public bool IsIdle(Vector2 moveInput) =>
            moveInput.x == 0.0f && moveInput.y == 0.0f;
    }
}