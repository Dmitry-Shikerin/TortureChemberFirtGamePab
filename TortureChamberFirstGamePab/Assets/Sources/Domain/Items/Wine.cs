using System;
using Sources.Domain.Items.ItemConfigs;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;
using UnityEngine;

namespace Sources.Domain.Items
{
    public class Wine : IItem
    {
        private readonly ItemConfig _itemConfig;

        public Wine(ItemConfig itemConfig)
        {
            _itemConfig = itemConfig ? itemConfig : throw new ArgumentNullException(nameof(itemConfig));
            Icon = itemConfig.Icon;
            Title = itemConfig.Title;
            Price = itemConfig.Price;
            WaitingTime = itemConfig.Price;
        }

        public IItemView ItemView { get; private set; }
        public Sprite Icon { get; }
        public string Title { get; }
        public int Price { get; }
        public float WaitingTime { get; }

        public void SetItemView(IItemView itemView) => 
            ItemView = itemView;

        public IItem Clone() => 
            new Wine(_itemConfig);
    }
}