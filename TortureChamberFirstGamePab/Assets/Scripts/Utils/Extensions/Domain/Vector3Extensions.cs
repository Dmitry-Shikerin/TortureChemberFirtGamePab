using Scripts.Domain.Data;
using UnityEngine;

namespace Scripts.Utils.Extensions.Domain
{
    public static class Vector3Extensions
    {
        public static Vector3Data ToVector3Data(this Vector3 vector)
        {
            return new Vector3Data { X = vector.x, Y = vector.y, Z = vector.z };
        }
    }
}