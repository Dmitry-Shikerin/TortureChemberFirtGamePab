using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.DomainInterfaces.Items;
using Scripts.Utils.Repositories.ItemRepository.Interfaces;

namespace Scripts.Infrastructure.Factories.Domains.Items
{
    public class ItemsFactory
    {
        private readonly IItemProvider<IItem> _itemProvider;

        private Dictionary<Type, IItem> _items;

        public ItemsFactory(IItemProvider<IItem> itemProvider)
        {
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
        }

        private Dictionary<Type, IItem> Items => _items ??=
            _itemProvider.Collection
                .ToDictionary(item => item.GetType(), item => item);

        public IItem Create<T>()
            where T : IItem
        {
            if (Items.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException(nameof(T));

            return Items[typeof(T)].Clone();
        }
    }
}