using UnityEngine;

namespace Sources.Domain.Players.Inputs
{
    public struct PlayerInput
    {
        public PlayerInput(Vector2 direction, bool isSimpleAxis)
        {
            Direction = direction;
            IsSimpleAxis = isSimpleAxis;
        }

        public Vector2 Direction { get; }
        public bool IsSimpleAxis { get; }
    }
}