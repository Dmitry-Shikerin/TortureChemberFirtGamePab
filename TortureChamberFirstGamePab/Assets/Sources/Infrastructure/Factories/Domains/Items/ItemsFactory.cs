using System;
using System.Collections.Generic;
using System.Linq;
using Sources.DomainInterfaces.Items;
using Sources.Utils.Repositoryes;
using Sources.Utils.Repositoryes.ItemRepository;

namespace Sources.Infrastructure.Factories.Domains.Items
{
    public class ItemsFactory
    {
        private readonly ItemProvider<IItem> _itemProvider;
        private Dictionary<Type, IItem> _items;

        public ItemsFactory(ItemProvider<IItem> itemProvider)
        {
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
        }

        private Dictionary<Type, IItem> Items => _items ??=
            _itemProvider.GetAll()
                .ToDictionary(item => item.GetType(), item => item);
        
        public IItem Create<T>() where T : IItem
        {
            if (Items.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException(nameof(T));

            return Items[typeof(T)].Clone();
        }

        private Dictionary<Type, IItem> Add(IEnumerable<IItem> items)
        {
            foreach (IItem item in items)
            {
                _items[item.GetType()] = item;
            }
            
            return _items;
        }
    }
}