using System;
using Sources.Domain.Items.ItemConfigs;
using Sources.DomainInterfaces.Items;
using Sources.PresentationInterfaces.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Domain.Items
{
    public class Beer : IItem
    {
        private readonly ItemConfig _itemConfig;

        public Beer(ItemConfig itemConfig)
        {
            _itemConfig = itemConfig;
            Icon = itemConfig.Icon;
            Title = itemConfig.Title;
            Price = itemConfig.Price;
            WaitingTime = itemConfig.Price;
        }

        // public IItemView ItemView { get; }
        public Sprite Icon { get; }
        public string Title { get; }
        public int Price { get; }
        public float WaitingTime { get; }

        public IItem Clone()
        {
            return new Beer(_itemConfig);
        }
    }
}