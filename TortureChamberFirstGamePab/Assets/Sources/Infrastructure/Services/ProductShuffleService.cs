using System;
using System.Collections.Generic;
using System.Linq;
using Sources.DomainInterfaces.Items;
using Random = UnityEngine.Random;

namespace Sources.Infrastructure.Services
{
    public class ProductShuffleService
    {
        //TODO возможно принимать не лист
        private readonly List<IItem> _items;

        public ProductShuffleService(List<IItem> items)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            _items = new List<IItem>(items);
        }

        public IItem GetRandomItem()
        {
            Shuffle();

            return _items.First();
        }
        
        public void Shuffle()
        {
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                int randomItem = Random.Range(0 ,_items.Count);
                IItem shuffledElement = _items[randomItem];

                _items[randomItem] = _items[i];
                _items[i] = shuffledElement;
            }
        }
    }
}