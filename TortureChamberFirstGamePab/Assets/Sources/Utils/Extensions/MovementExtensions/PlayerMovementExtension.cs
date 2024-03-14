using UnityEngine;

namespace Sources.Utils.Extensions.MovementExtensions
{
    public static class PlayerMovementExtension
    {
        public static bool Approximately(this Vector2 first)
        {
            return Mathf.Approximately(first.sqrMagnitude, 0);
        }
    }
}