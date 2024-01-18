using Sources.Domain.Data;
using UnityEngine;

namespace Sources.Extensions.Domain
{
    public static partial class Vector3Extensions
    {
        public static Vector3Data Vector3ToVector3Data(this Vector3 vector) => 
            new Vector3Data() { X = vector.x, Y = vector.y, Z = vector.z };

        public static Vector3 Vector3DataToVector3(this Vector3Data vector3Data) => 
            new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);
    }
}