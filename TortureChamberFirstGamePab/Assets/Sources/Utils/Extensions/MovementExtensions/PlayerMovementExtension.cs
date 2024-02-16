using Sources.Domain.Players.Inputs;
using UnityEngine;

namespace Sources.Utils.Extensions.MovementExtensions
{
    public static partial class PlayerMovementExtension
    {
        public static bool Approximately(this Vector2 first) => 
            Mathf.Approximately(first.sqrMagnitude, 0);
    }
}