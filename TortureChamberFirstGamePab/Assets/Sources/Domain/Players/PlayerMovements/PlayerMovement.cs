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
            
            Position = data.Position.ToVector3();
            RotationAngle = data.Direction;
            Debug.Log(data.Position.ToVector3());
        }

        public PlayerMovement()
        {
        }
        
        private readonly IUpgradeble _upgradeble;
        private readonly PlayerMovementCharacteristic _characteristic;
        
        public float RotationAngle { get; set; }
        public Vector3 Position { get; set; }

        public bool IsIdle(Vector2 moveInput) =>
            moveInput.x == 0.0f && moveInput.y == 0.0f;
    }
}