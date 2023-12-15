using Sources.Domain.Items.ItemConfigs;
using Sources.DomainInterfaces.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Domain.Items
{
    public class Beer : IItem
    {
        public Beer(ItemConfig itemConfig)
        {
            Icon = itemConfig.Icon;
            Title = itemConfig.Title;
            Price = itemConfig.Price;
            WaitingTime = itemConfig.Price;
        }
        
        public Sprite Icon { get; }
        public string Title { get; }
        public int Price { get; }
        public float WaitingTime { get; }
    }
}