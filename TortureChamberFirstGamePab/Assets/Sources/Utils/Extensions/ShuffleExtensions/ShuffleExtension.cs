using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Sources.Utils.Extensions.ShuffleExtensions
{
    public static class ShuffleExtension
    {
        public static T GetRandomItemFromShuffle<T>(this IEnumerable<T> objects)
        {
            if (objects == null)
                throw new InvalidOperationException(nameof(objects));

            var enumerable = objects as T[] ?? objects.ToArray();

            if (enumerable.Any() == false)
                throw new InvalidOperationException(nameof(objects));

            enumerable.Shuffle();

            return enumerable.First();
        }

        public static T GetRandomItem<T>(this IEnumerable<T> objects)
        {
            if (objects == null)
                throw new InvalidOperationException(nameof(objects));

            var enumerable = objects as T[] ?? objects.ToArray();

            if (enumerable.Length == 0)
                throw new Exception("нечего перемешивать");

            if (enumerable.Any(@object => @object == null))
                throw new InvalidOperationException(nameof(enumerable));

            var index = Random.Range(0, enumerable.Length);

            if (enumerable[index] == null)
                throw new InvalidOperationException(nameof(enumerable));

            return enumerable[index];
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> objects)
        {
            var enumerable = objects as T[] ?? objects.ToArray();

            for (var i = 0; i < enumerable.Length; i++)
            {
                var randomItem = Random.Range(0, enumerable.Length);
                (enumerable[randomItem], enumerable[i]) = (enumerable[i], enumerable[randomItem]);
            }

            return enumerable;
        }

        public static bool GetRandomChance(int positiveRange, int maximumRange)
        {
            return positiveRange >= Random.Range(0, maximumRange);
        }
    }
}