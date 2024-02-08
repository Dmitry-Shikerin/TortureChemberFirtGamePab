﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sources.InfrastructureInterfaces.Services.ShuffleServices;
using Random = UnityEngine.Random;

namespace Sources.Infrastructure.Services.ShuffleServices
{
    public abstract class ShuffleService<T> : IShuffleService<T>
    {
        protected List<T> Items { get; private set; }

        //TODO возможно переделать на экстеншн
        public T GetRandomItem()
        {
            //TODO как это исправить
            if (Items == null) 
                Items = GetItems();
            
            Shuffle();

            if (Items.Count <= 0)
                throw new InvalidOperationException(nameof(Items));
            
            return Items.First();
        }

        private void Shuffle()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                int randomItem = Random.Range(0 , Items.Count);
                (Items[randomItem], Items[i]) = (Items[i], Items[randomItem]);
            }
        }

        protected abstract List<T> GetItems();
    }
}