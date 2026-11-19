using System;
using Domain.DataAccess.PlayerData;
using UnityEngine;

namespace Domain.Players.PlayerMovements
{
    public class PlayerMovement
    {
        public PlayerMovement(MovementData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Position = new Vector3(data.Position.X, data.Position.Y, data.Position.Z);
            HasSavedPosition = true;
            RotationAngle = data.Direction;
        }

        public PlayerMovement()
        {
        }

        public bool HasSavedPosition { get; }
        public float RotationAngle { get; set; }
        public Vector3 Position { get; set; }
        public float Speed { get; set; }
        public float AnimationSpeed { get; set; }
    }
}