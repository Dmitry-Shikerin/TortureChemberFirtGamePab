using System;
using System.Collections.Generic;
using Sources.DomainInterfaces.Items;

namespace Sources.Infrastructure.Factories.Domains.Items
{
    public class ItemsFactory
    {
        private Dictionary<Type, IItem> _items = new Dictionary<Type, IItem>();

        public ItemsFactory(IEnumerable<IItem> items)
        {
            Add(items);
        }

        public IItem Create<T>() where T : IItem
        {
            if (_items.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException(nameof(T));

            return _items[typeof(T)].Clone();
        }

        private void Add(IEnumerable<IItem> items)
        {
            foreach (IItem item in items)
            {
                _items[item.GetType()] = item;
            }
        }
    }
}