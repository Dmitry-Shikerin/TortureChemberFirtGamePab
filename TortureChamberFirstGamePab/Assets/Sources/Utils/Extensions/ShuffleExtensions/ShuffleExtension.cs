using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Utils.Extensions.ShuffleExtensions
{
    public static partial class ShuffleExtension
    {
        public static T GetRandomItemFromShuffle<T>(this IEnumerable<T> objects)
        {
            if (objects == null) 
                throw new InvalidOperationException(nameof(objects));
            
            T[] enumerable = objects as T[] ?? objects.ToArray();
            
            if (enumerable.Any() == false)
                throw new InvalidOperationException(nameof(objects));
            
            enumerable.Shuffle();
            
            return enumerable.First();
        }

        //TODO вылетает ошибка индекса
        public static T GetRandomItem<T>(this IEnumerable<T> objects)
        {
            if (objects == null) 
                throw new InvalidOperationException(nameof(objects));
            
            T[] enumerable = objects as T[] ?? objects.ToArray();

            if (enumerable.Length == 0)
                throw new Exception("нечего перемешивать");
            
            if(enumerable.Any(@object => @object == null))
                throw new InvalidOperationException(nameof(enumerable));

            //TODO сделал коррекцию
            //TODO maxExclude берется включительно?
            int index = Random.Range(0, enumerable.Length);

            // Debug.Log(index);
            
            if (enumerable[index] == null)
                throw new InvalidOperationException(nameof(enumerable));
            
            return enumerable[index];
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
    }
}