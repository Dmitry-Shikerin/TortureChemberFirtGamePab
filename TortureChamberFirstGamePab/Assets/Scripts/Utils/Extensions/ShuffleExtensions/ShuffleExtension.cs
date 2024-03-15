using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Scripts.Utils.Extensions.ShuffleExtensions
{
    public static class ShuffleExtension
    {
        public static T GetRandomItem<T>(this IEnumerable<T> objects)
        {
            if (objects == null)
                throw new InvalidOperationException(nameof(objects));

            var enumerable = objects as T[] ?? objects.ToArray();

            if (enumerable.Length == 0)
                throw new Exception();

            if (enumerable.Any(@object => @object == null))
                throw new InvalidOperationException(nameof(enumerable));

            var index = Random.Range(0, enumerable.Length);

            if (enumerable[index] == null)
                throw new InvalidOperationException(nameof(enumerable));

            return enumerable[index];
        }

        public static bool GetRandomChance(int positiveRange, int maximumRange) =>
            positiveRange >= Random.Range(0, maximumRange);
    }
}