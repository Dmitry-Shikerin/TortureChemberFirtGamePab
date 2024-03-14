﻿using Sources.Domain.Data;
using UnityEngine;

namespace Sources.Extensions.Domain
{
    public static class Vector2Extensions
    {
        public static Vector2Data Vector2ToVector2Data(this Vector2 vector)
        {
            return new Vector2Data { X = vector.x, Y = vector.y };
        }

        public static Vector2 Vector2DataToVector2(this Vector3Data vector3Data)
        {
            return new Vector2(vector3Data.X, vector3Data.Y);
        }
    }
}