using System;
using System.Collections.Generic;
using Sources.DomainInterfaces.Items;
using Sources.Utils.Repositoryes.ItemRepository;

namespace Sources.Infrastructure.Services.ShuffleServices
{
    public class ItemShuffleService : ShuffleService<IItem>
    {
        private readonly ItemProvider<IItem> _itemProvider;
        private List<IItem> _items;
        
        public ItemShuffleService(ItemProvider<IItem> itemProvider)
        {
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
        }
        
        protected override List<IItem> GetItems() => 
            _items ??= new List<IItem>(_itemProvider.Collection);
    }
}