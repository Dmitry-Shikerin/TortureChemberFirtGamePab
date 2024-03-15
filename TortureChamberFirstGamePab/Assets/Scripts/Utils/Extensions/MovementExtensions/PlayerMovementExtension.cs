using UnityEngine;

namespace Scripts.Utils.Extensions.MovementExtensions
{
    public static class PlayerMovementExtension
    {
        public static bool Approximately(this Vector2 first) =>
            Mathf.Approximately(first.sqrMagnitude, 0);
    }
}