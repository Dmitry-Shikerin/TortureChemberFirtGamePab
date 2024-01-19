using System;
using Sources.Domain.Players.PlayerMovements.PlayerMovementCharacteristics;
using Sources.DomainInterfaces.Upgrades;
using Sources.Extensions.Domain;
using Sources.Infrastructure.Services.LoadServices.DataAccess;
using UnityEngine;

namespace Sources.Domain.Players.PlayerMovements
{
    public class PlayerMovement
    {
        public PlayerMovement(PlayerMovementData data)
        {
            if (data == null) 
                throw new ArgumentNullException(nameof(data));
            
            Position = data.Position.Vector3DataToVector3();
            Debug.Log(data.Position.Vector3DataToVector3());
        }

        public PlayerMovement()
        {
        }
        
        private readonly IUpgradeble _upgradeble;
        private readonly PlayerMovementCharacteristic _characteristic;

        private Vector3 _position;

        public event Action PositionChanged;

        //TODO это не позишн а директион
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