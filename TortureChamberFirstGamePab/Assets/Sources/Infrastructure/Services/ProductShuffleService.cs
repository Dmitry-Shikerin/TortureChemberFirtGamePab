using System;
using System.Collections.Generic;
using System.Linq;
using Sources.DomainInterfaces.Items;
using Sources.Utils.Repositoryes;
using Random = UnityEngine.Random;

namespace Sources.Infrastructure.Services
{
    public class ProductShuffleService
    {
        private readonly List<IItem> _items;

        public ProductShuffleService(ItemRepository<IItem> itemRepository)
        {
            if (itemRepository == null) 
                throw new ArgumentNullException(nameof(itemRepository));
            // if(items == null)
            //     throw new ArgumentNullException(nameof(items));
            //
            // if (items.Count() <= 0)
            //     throw new ArgumentOutOfRangeException(nameof(items));

            _items = new List<IItem>(itemRepository.GetAll());
        }

        public IItem GetRandomItem()
        {
            Shuffle();

            if (_items.Count <= 0)
                throw new InvalidOperationException(nameof(_items));
            
            return _items.First();
        }

        private void Shuffle()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                int randomItem = Random.Range(0 ,_items.Count);
                (_items[randomItem], _items[i]) = (_items[i], _items[randomItem]);
            }
        }
    }
}