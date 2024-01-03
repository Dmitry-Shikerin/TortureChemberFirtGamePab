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

        //TODO возможно тут возвращать Type и у посетителя тоже запрашивать тип
        public IItem GetRandomItem()
        {
            Shuffle();

            return _items.First();
        }

        private void Shuffle()
        {
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                //TODO посмотреть про исправление этой записи
                int randomItem = Random.Range(0 ,_items.Count);
                (_items[randomItem], _items[i]) = (_items[i], _items[randomItem]);
            }
        }
    }
}