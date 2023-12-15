using System;
using JetBrains.Annotations;
using Sources.Domain.Items;
using Sources.Domain.Items.ItemConfigs;
using Sources.DomainInterfaces.Items;
using Sources.Utils.Repositoryes;
using UnityEngine;

namespace Sources.Infrastructure.Factorys.Domains.Items
{
    public class ItemsFactory
    {
        private const string BeerItemConfigPath = "Configs/Items/BeerItemConfig";
        
        private readonly ItemRepository<IItem> _itemRepository;

        public ItemsFactory(ItemRepository<IItem> itemRepository)
        {
            _itemRepository = itemRepository ?? 
                              throw new ArgumentNullException(nameof(itemRepository));
        }

        //TODO ничего не возвращает!!!
        public void Create()
        {
            ItemConfig beerConfig = Resources.Load<ItemConfig>(BeerItemConfigPath); 
            Beer beer = new Beer(beerConfig);
            _itemRepository.Add(beer);
        }
    }
}