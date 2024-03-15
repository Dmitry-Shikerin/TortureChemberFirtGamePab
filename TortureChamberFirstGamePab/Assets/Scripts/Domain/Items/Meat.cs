using Scripts.Domain.Items.ItemConfigs;
using Scripts.DomainInterfaces.Items;
using Scripts.PresentationInterfaces.Views.Items;
using UnityEngine;

namespace Scripts.Domain.Items
{
    public class Meat : IItem
    {
        private readonly ItemConfig _itemConfig;

        public Meat(ItemConfig itemConfig)
        {
            _itemConfig = itemConfig;
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

        public void SetItemView(IItemView itemView)
        {
            ItemView = itemView;
        }

        public IItem Clone()
        {
            return new Meat(_itemConfig);
        }
    }
}