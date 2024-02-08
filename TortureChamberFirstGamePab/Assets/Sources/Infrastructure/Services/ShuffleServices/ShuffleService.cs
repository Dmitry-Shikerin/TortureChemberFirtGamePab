using System;
using System.Collections.Generic;
using System.Linq;
using Sources.DomainInterfaces.Items;
using Sources.InfrastructureInterfaces.Services.ShuffleServices;
using Sources.Utils.Repositoryes.ItemRepository;
using Random = UnityEngine.Random;

namespace Sources.Infrastructure.Services.ShuffleServices
{
    public abstract class ShuffleService<T> : IShuffleService<T>
    {
        protected List<T> Items { get; private set; }

        public T GetRandomItem()
        {
            //TODO как это исправить
            CheckAvailabilityItems();
            
            Shuffle();

            if (Items.Count <= 0)
                throw new InvalidOperationException(nameof(Items));
            
            return Items.First();
        }

        private void Shuffle()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                int randomItem = Random.Range(0 ,Items.Count);
                (Items[randomItem], Items[i]) = (Items[i], Items[randomItem]);
            }
        }

        private void CheckAvailabilityItems()
        {
            if (Items == null)
            {
                Items = GetItems();
            }
        }

        protected abstract List<T> GetItems();
    }
}