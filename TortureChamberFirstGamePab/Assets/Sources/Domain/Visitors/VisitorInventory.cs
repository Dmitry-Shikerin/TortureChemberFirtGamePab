using Sources.DomainInterfaces.Items;
using UnityEngine;

namespace Sources.Domain.Visitors
{
    public class VisitorInventory
    {
        public IItem Item { get; private set; }
        public IItem TargetItem { get; private set; }

        public void TakeItem(IItem item)
        {
            Item = item;
            // Debug.Log(Item.GetType().Name);
        }
        
        public void SetTargetItem(IItem targetItem)
        {
            TargetItem = targetItem;
        }
    }
}