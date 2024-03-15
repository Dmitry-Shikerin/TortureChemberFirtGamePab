using System;
using Scripts.Domain.DataAccess.PlayerData;
using UnityEngine;

namespace Scripts.Domain.Players.PlayerMovements
{
    public class PlayerMovement
    {
        public PlayerMovement(MovementData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            RotationAngle = data.Direction;
        }

        public PlayerMovement()
        {
        }

        public float RotationAngle { get; set; }
        public Vector3 Position { get; set; }
        public float Speed { get; set; }
        public float AnimationSpeed { get; set; }
    }
}