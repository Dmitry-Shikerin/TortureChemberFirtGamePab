using System;
using System.Collections.Generic;
using Sources.DomainInterfaces.Items;
using Sources.Utils.Repositoryes.ItemRepository;
using Sources.Utils.Repositoryes.ItemRepository.Interfaces;

namespace Sources.Infrastructure.Services.ShuffleServices
{
    public class ItemShuffleService : ShuffleService<IItem>
    {
        private readonly IItemProvider<IItem> _itemProvider;
        private List<IItem> _items;
        
        public ItemShuffleService(IItemProvider<IItem> itemProvider)
        {
            _itemProvider = itemProvider ?? throw new ArgumentNullException(nameof(itemProvider));
        }
        
        protected override List<IItem> GetItems() => 
            _items ??= new List<IItem>(_itemProvider.Collection);
    }
}