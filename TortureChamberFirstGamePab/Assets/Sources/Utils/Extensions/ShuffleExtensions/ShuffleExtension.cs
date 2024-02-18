using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Sources.Utils.Extensions.ShuffleExtensions
{
    public static partial class ShuffleExtension
    {
        public static T GetRandomItem<T>(this IEnumerable<T> objects)
        {
            if (objects == null) 
                throw new InvalidOperationException(nameof(objects));
            
            T[] enumerable = objects as T[] ?? objects.ToArray();
            
            if (enumerable.Any() == false)
                throw new InvalidOperationException(nameof(objects));
            
            enumerable.Shuffle();
            
            return enumerable.First();
        }
        
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> objects)
        {
            T[] enumerable = objects as T[] ?? objects.ToArray();
            
            for (int i = 0; i < enumerable.Length; i++)
            {
                int randomItem = Random.Range(0 , enumerable.Length);
                (enumerable[randomItem], enumerable[i]) = (enumerable[i], enumerable[randomItem]);
            }
            
            return enumerable;
        }

        public static bool GetRandomChance(int positiveRange, int maximumRange) => 
            positiveRange >= Random.Range(0, maximumRange);

        //TODO сделать рандомайзер
    }
}