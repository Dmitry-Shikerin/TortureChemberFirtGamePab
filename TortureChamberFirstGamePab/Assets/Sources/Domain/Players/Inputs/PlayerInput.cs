using UnityEngine;

namespace Sources.Domain.Players.Inputs
{
    public struct PlayerInput
    {
        public PlayerInput(Vector2 direction)
        {
            Direction = direction;
        }

        public Vector2 Direction { get; }
    }
}