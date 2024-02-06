using System;
using System.Collections.Generic;
using System.Linq;
using Sources.DomainInterfaces.Items;
using Sources.Utils.Repositoryes;
using Sources.Utils.Repositoryes.ItemRepository;
using Random = UnityEngine.Random;

namespace Sources.Infrastructure.Services
{
    public class ProductShuffleService
    {
        private readonly ItemProvider<IItem> _itemProvider;
        private List<IItem> _items;

        public ProductShuffleService(ItemProvider<IItem> itemProvider)
        {
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
        }
        
        private List<IItem> Items => _items ??= new List<IItem>(_itemProvider.Collection);

        public IItem GetRandomItem()
        {
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
    }
}